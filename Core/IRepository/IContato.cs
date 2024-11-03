using Core.Entity;

namespace Core.IRepository
{
    public interface IContato
    {

        Task<IEnumerable<ContatoModel>> GetContatos();
        Task<bool> CadastrarContatoAsync(ContatoModel contato, string cep);

    }
}
