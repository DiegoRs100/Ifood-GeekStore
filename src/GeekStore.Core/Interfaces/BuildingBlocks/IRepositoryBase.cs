using GeekStore.Core.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GeekStore.Core.Interfaces.BuildingBlocks
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase
    {
        void Insert(TEntity entity);
        Task InsertAndSave(TEntity entity);

        void Update(TEntity entity);
        Task UpdateAndSave(TEntity entity);

        void Delete(TEntity entity);
        Task DeleteAndSave(TEntity entity);

        Task<TEntity> ObterPorId(Guid id, bool tracking = false);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task<IEnumerable<TEntity>> ObterPorFiltro(Expression<Func<TEntity, bool>> expressao);

        Task<int> SaveChanges();
    }
}