using Core.Entity;
using Core.IRepository;
using Microsoft.AspNetCore.Mvc;

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
        /// Obtém todos os contatos registrados.
        /// </summary>
        /// <remarks>
        /// Exemplo de resposta:
        ///
        ///     GET /Contato
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

        [HttpGet]
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
        /// Cadastra um novo contato, puxando o DDD de forma automatica
        /// </summary>
        /// <param name="=cep">CEP do contato para puxar o DDD</param>
        /// <param name="=contato">Dados do contato</param>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///       {
        ///         "contatoNome": "Daniel",
        ///         "contatoEmail": "daniel@dd.com",
        ///         "contatoNumero": "939524211" -Sem o DDD
        ///       }
        /// 
        /// </remarks>
        /// <returns>Resultado da operação</returns>
        [HttpPost]
        public async Task<IActionResult> CreateContato([FromBody] ContatoModel contato, [FromQuery] string cep)
        {
            try
            {
                await _contatoRepo.CadastrarContatoAsync(contato, cep);
                return Ok(contato);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
