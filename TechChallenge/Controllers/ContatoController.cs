using Core.Entity;
using Core.Input;
using Core.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TechChallenge.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ContatoController : ControllerBase
    {

        private readonly IContato _contatoRepo;

        public ContatoController(IContato contatoRepo)
        {
            _contatoRepo = contatoRepo;
        }

        /// <summary>
        /// Cadastra um novo contato, puxando o DDD de forma automatica
        /// </summary>
        /// <param name="=contato">Dados do contato</param>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///       {
        ///         "contatoNome": "Daniel",
        ///         "contatoEmail": "daniel@dd.com",
        ///         "contatoNumero": "939524211" -Sem o DDD
        ///         "cep": "09270490"
        ///       }
        /// 
        /// </remarks>
        /// <returns>Resultado da operação</returns>
        [HttpPost]
        public async Task<IActionResult> CadastrarContato([FromBody] ContatoDto contatoRequest)
        {
            try
            {
                //DTO -> Model
                var contato = new ContatoModel
                {
                    ContatoNome = contatoRequest.ContatoNome,
                    ContatoEmail = contatoRequest.ContatoEmail,
                    ContatoNumero = contatoRequest.ContatoNumero
                };

                await _contatoRepo.CadastrarContatoAsync(contato, contatoRequest.Cep);
                return Ok("Contato cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtém todos os contatos registrados com os DDDs correspondentes.
        /// </summary>
        /// <param name="ddd">Lista de DDDs separados por vírgula (ex: 11,21,31)</param>
        /// <remarks>
        /// Os DDDs por região:
        /// 
        /// Centro-Oeste
        /// - Distrito Federal (61)
        /// - Goiás (62, 64)
        /// - Mato Grosso (65, 66)
        /// - Mato Grosso do Sul (67)
        /// 
        /// Nordeste
        /// - Alagoas (82)
        /// - Bahia (71, 73, 74, 75, 77)
        /// - Ceará (85, 88)
        /// - Maranhão (98, 99)
        /// - Paraíba (83)
        /// - Pernambuco (81, 87)
        /// - Piauí (86, 89)
        /// - Rio Grande do Norte (84)
        /// - Sergipe (79)
        /// 
        /// Norte
        /// - Acre (68)
        /// - Amapá (96)
        /// - Amazonas (92, 97)
        /// - Pará (91, 93, 94)
        /// - Rondônia (69)
        /// - Roraima (95)
        /// - Tocantins (63)
        /// 
        /// Sudeste
        /// - Espírito Santo (27, 28)
        /// - Minas Gerais (31, 32, 33, 34, 35, 37, 38)
        /// - Rio de Janeiro (21, 22, 24)
        /// - São Paulo (11, 12, 13, 14, 15, 16, 17, 18, 19)
        /// 
        /// Sul
        /// - Paraná (41, 42, 43, 44, 45, 46)
        /// - Rio Grande do Sul (51, 53, 54, 55)
        /// - Santa Catarina (47, 48, 49)
        /// 
        /// Exemplo de resposta:
        ///
        ///     [
        ///       {
        ///         "id": 1,
        ///         "dataCriacao": "2024-11-02T13:24:12.646667",
        ///         "contatoNome": "Daniel",
        ///         "contatoEmail": "daniel@dd.com",
        ///         "contatoDDD": 11,
        ///         "contatoNumero": "939524211"
        ///       }
        ///     ]
        /// </remarks>
        /// <returns>Lista de contatos de acordo com os DDDs informados</returns>
        /// <response code="200">Retorna a lista de contatos com os DDDs correspondentes</response>
        /// <response code="400">Se ocorrer um erro ao obter os contatos</response>
        [HttpGet("GetByDdd")]
        public async Task<IActionResult> GetContatosByDdd([FromQuery] string ddd)
        {
            try
            {
                var dddList = ddd.Split(',').Select(int.Parse).ToList();
                var contatos = await _contatoRepo.GetContatosByDdd(dddList);
                return Ok(contatos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        /// <summary>
        /// Obtém todos os contatos registrados.
        /// </summary>
        /// <remarks>
        /// Exemplo de resposta:
        ///
        ///     [
        ///       {
        ///         "id": 1,
        ///         "dataCriacao": "2024-11-02T13:24:12.646667",
        ///         "contatoNome": "Daniel",
        ///         "contatoEmail": "daniel@dd.com",
        ///         "contatoDDD": 11,
        ///         "contatoNumero": "939524211"
        ///       }
        ///     ]
        ///
        /// </remarks>
        /// <returns>Lista de contatos</returns>
        /// <response code="200">Retorna a lista de contatos</response>
        /// <response code="400">Se ocorrer um erro ao obter os contatos</response>

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetContatos()
        {
            try
            {
                var contatos = await _contatoRepo.GetContatos();
                return Ok(contatos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
       

        /// <summary>
        /// Deleta um contato pelo Id.
        /// </summary>
        /// <param name="id">Id do contato a ser deletado</param>
        /// <returns>Mensagem de sucesso ou erro</returns>
        /// <response code="200">Contato deletado com sucesso</response>
        /// <response code="404">Contato não encontrado</response>
        /// <response code="400">Se ocorrer um erro ao deletar o contato</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContatoById(int id)
        {
            try
            {
                var deleted = await _contatoRepo.DeleteContatoById(id);

                if (deleted)
                    return Ok("Contato deletado com sucesso.");

                return NotFound("Contato não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um contato existente pelo Id.
        /// </summary>
        /// <param name="id">Id do contato a ser atualizado</param>
        /// <param name="contatoRequest">Dados do contato a serem atualizados</param>
        /// <returns>Mensagem de sucesso ou erro</returns>
        /// <response code="200">Contato atualizado com sucesso</response>
        /// <response code="404">Contato não encontrado</response>
        /// <response code="400">Se ocorrer um erro ao atualizar o contato</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContato(int id, [FromBody] ContatoDto contatoRequest)
        {
            try
            {
                var contato = new ContatoModel
                {
                    Id = id,
                    ContatoNome = contatoRequest.ContatoNome,
                    ContatoEmail = contatoRequest.ContatoEmail,
                    ContatoNumero = contatoRequest.ContatoNumero
                };

                var updated = await _contatoRepo.UpdateContato(contato, contatoRequest.Cep);

                if (updated)
                    return Ok("Contato atualizado com sucesso.");

                return NotFound("Contato não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
