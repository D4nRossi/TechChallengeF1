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

        public async Task<bool> CadastrarContatoAsync(ContatoModel contato, string cep)
        {

            //Formatando o CEP
            cep = cep.Replace("-", "");

            //Validar o email
            if (!IsValidEmail(contato.ContatoEmail))
            {
                throw new Exception("Email Inválido");
            }

            //Validar número de telefone 
            if (!contato.ContatoNumero.StartsWith("9"))
            {
                throw new Exception("Número de telefone inválido: o número não pode incluir o DDD e deve começar com '9'.");
            }

            //Achar o DDD pela região - Usando o Id IBGE como parametro
            var viacepResponse = await _httpClient.GetStringAsync($"{_viaCepUrl}{cep}/json/");
            var viaCepData = JsonSerializer.Deserialize<ViaCepModel>(viacepResponse);

            if (viaCepData == null || string.IsNullOrEmpty(viaCepData.Ddd))
            {
                throw new Exception("DDD não encontrado para o CEP informado.");
            }

            
            //Data Criacao sempre getdate
            contato.DataCriacao = DateTime.Now;

            //Salvar o contato com o DDD
            contato.ContatoDDD = int.Parse(viaCepData.Ddd);

            _context.Contato.Add(contato);
            await _context.SaveChangesAsync();

            return true;
        }




        //Metodos Auxiliares
        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
    }
}
