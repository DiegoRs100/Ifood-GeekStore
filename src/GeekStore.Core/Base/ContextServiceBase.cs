using GeekStore.Core.Interfaces.BuildingBlocks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GeekStore.Core.Base
{
    public abstract class ContextServiceBase<TContext> : IContextServiceBase where TContext : DbContext
    {
        #region Injection

        protected readonly TContext Context;

        protected ContextServiceBase(TContext database)
        {
            Context = database;
        }

        #endregion

        public async Task CreateTransaction()
        {
            await Context.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            Context.Database.CurrentTransaction.Commit();
        }

        public void Rollback()
        {
            Context.Database.CurrentTransaction.Rollback();
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