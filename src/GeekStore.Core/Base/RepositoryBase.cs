using Microsoft.EntityFrameworkCore;
using GeekStore.Core.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GeekStore.Core.Interfaces.BuildingBlocks;

namespace GeekStore.Core.Base
{
    public abstract class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : EntityBase, new()
        where TContext : DbContext
    {
        #region Injection

        protected readonly TContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected RepositoryBase(TContext database)
        {
            Context = database;
            DbSet = database.Set<TEntity>();
        }

        #endregion

        public async Task<IEnumerable<TEntity>> ObterPorFiltro(Expression<Func<TEntity, bool>> expressao)
        {
            return await DbSet.AsNoTracking()
                .Where(expressao).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id, bool tracking = false)
        {
            if (tracking)
                return await DbSet.FindAsync(id);
            else
                return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await DbSet.AsNoTracking()
               .Where(x => x.Ativo).ToListAsync();
        }

        public virtual void Insert(TEntity entity)
        {
            entity.Ativar();
            DbSet.Add(entity);
        }

        public virtual async Task InsertAndSave(TEntity entity)
        {
            Insert(entity);
            await SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            entity.Ativar();
            DbSet.Update(entity);
        }

        public virtual async Task UpdateAndSave(TEntity entity)
        {
            Update(entity);
            await SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            entity.Inativar();
            DbSet.Update(entity);
        }

        public virtual async Task DeleteAndSave(TEntity entity)
        {
            Delete(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Context?.Dispose();
        }
    }
}