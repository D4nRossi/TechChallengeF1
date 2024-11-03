using Core.Entity;

namespace Core.IRepository
{
    public interface IContato
    {

        Task<IEnumerable<ContatoModel>> GetContatos();
        Task<IEnumerable<ContatoModel>> GetContatosByDdd(List<int> dddList);

        Task<bool> CadastrarContatoAsync(ContatoModel contato, string cep);
        Task<bool> DeleteContatoById(int id);
        Task<bool> UpdateContato(ContatoModel contato, string cep);


    }
}
