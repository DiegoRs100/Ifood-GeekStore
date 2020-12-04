using GeekStore.Core.Dto_s;
using System.Threading.Tasks;

namespace GeekStore.Core.Interfaces.BuildingBlocks
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> Inserir(TEntity entity);
        Task<TEntity> Atualizar(TEntity entity);
        Task<bool> Remover(TEntity entity);
    }
}