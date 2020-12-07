using GeekStore.Business.Dto_s;
using GeekStore.Core.Interfaces.BuildingBlocks;
using System.Threading.Tasks;

namespace GeekStore.Business.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<bool> VerificarExistencia(string nomeProduto);
    }
}