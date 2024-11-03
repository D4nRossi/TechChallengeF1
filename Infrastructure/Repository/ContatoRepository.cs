using Core.Entity;
using Core.IRepository;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Infrastructure.Repository
{
    public class ContatoRepository : IContato
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _viaCepUrl;


        public ContatoRepository(AppDbContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _viaCepUrl = configuration["ApiSettings:ViaCepUrl"];
        }

        public async Task<bool> CadastrarContatoAsync(ContatoModel contato, string cep)
        {
            // Validações
            ContatoValidator.ValidateCep(ref cep);
            ContatoValidator.ValidateEmail(contato.ContatoEmail);
            ContatoValidator.ValidatePhoneNumber(contato.ContatoNumero);

            try
            {
                // Achar o DDD pela região usando o CEP
                var response = await _httpClient.GetAsync($"{_viaCepUrl}{cep}/json/");

                // Verifica a resposta 
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Falha ao acessar a API ViaCEP: {response.ReasonPhrase}");
                }

                var responseData = await response.Content.ReadAsStringAsync();
                var viaCepData = JsonSerializer.Deserialize<ViaCepModel>(responseData);

                if (viaCepData == null || string.IsNullOrEmpty(viaCepData.Ddd))
                {
                    throw new Exception("DDD não encontrado para o CEP informado. Verifique se o CEP é válido.");
                }

                // Configura o DDD do contato
                contato.ContatoDDD = int.Parse(viaCepData.Ddd);

                // Data de criação
                contato.DataCriacao = DateTime.Now;

                _context.Contato.Add(contato);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Erro de conexão com a API ViaCEP. Verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar contato: {ex.Message}");
            }
        }


        public async Task<IEnumerable<ContatoModel>> GetContatos()
        {
            #region Dapper
            /*var query = @"
                SELECT 
                    CTT_ID AS Id,
                    CTT_DTCRIACAO AS DataCriacao,
                    CTT_NOME AS ContatoNome,
                    CTT_EMAIL AS ContatoEmail,
                    CTT_DDD AS ContatoDDD,
                    CTT_NUMERO AS ContatoNumero
                FROM CONTATO_CTT (NOLOCK)";

            using (var connection = _context.Database.GetDbConnection())
            {
                return await connection.QueryAsync<ContatoModel>(query);
            }*/
            #endregion

            #region EF Core
            return await _context.Contato.ToListAsync();
            #endregion
        }

        public async Task<IEnumerable<ContatoModel>> GetContatosByDdd(List<int> dddList)
        {
            var query = @"
                SELECT 
                    CTT_ID AS Id,
                    CTT_DTCRIACAO AS DataCriacao,
                    CTT_NOME AS ContatoNome,
                    CTT_EMAIL AS ContatoEmail,
                    CTT_DDD AS ContatoDDD,
                    CTT_NUMERO AS ContatoNumero
                FROM CONTATO_CTT (NOLOCK) 
                WHERE CTT_DDD IN @DddList";

            using (var connection = _context.Database.GetDbConnection())
            {
                return await connection.QueryAsync<ContatoModel>(query, new { DddList = dddList });
            }
        }


       


        public async Task<bool> DeleteContatoById(int id)
        {
            var query = "DELETE FROM CONTATO_CTT WHERE CTT_ID = @Id";

            using (var connection = _context.Database.GetDbConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
                return affectedRows > 0;
            }
        }

        public async Task<bool> UpdateContato(ContatoModel contato, string cep)
        {
            // Validações
            ContatoValidator.ValidateCep(ref cep);
            ContatoValidator.ValidateEmail(contato.ContatoEmail);
            ContatoValidator.ValidatePhoneNumber(contato.ContatoNumero);

            // Achar o DDD pela região
            var viacepResponse = await _httpClient.GetStringAsync($"{_viaCepUrl}{cep}/json/");
            var viaCepData = JsonSerializer.Deserialize<ViaCepModel>(viacepResponse);

            if (viaCepData == null || string.IsNullOrEmpty(viaCepData.Ddd))
            {
                throw new Exception("DDD não encontrado para o CEP informado.");
            }

            // Atualizar o DDD do contato
            contato.ContatoDDD = int.Parse(viaCepData.Ddd);

            // Realizar o update no banco de dados
            var query = @"
                UPDATE CONTATO_CTT
                SET 
                    CTT_NOME = @ContatoNome,
                    CTT_EMAIL = @ContatoEmail,
                    CTT_NUMERO = @ContatoNumero,
                    CTT_DDD = @ContatoDDD
                WHERE CTT_ID = @Id";

            using (var connection = _context.Database.GetDbConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, contato);
                return affectedRows > 0;
            }
        }
    }
}
