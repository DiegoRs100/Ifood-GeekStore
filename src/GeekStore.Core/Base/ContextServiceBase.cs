using GeekStore.Core.Interfaces.BuildingBlocks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GeekStore.Core.Base
{
    public abstract class ContextServiceBase<TContext> : IContextServiceBase where TContext : DbContext
    {
        #region Properties

        public Guid? IdTransaction { get; private set; }

        #endregion

        #region Injection

        protected readonly TContext Context;

        protected ContextServiceBase(TContext database)
        {
            Context = database;
        }

        #endregion

        public async Task<Guid?> CreateTransaction()
        {
            if (IdTransaction != null)
                return null;

            await Context.Database.BeginTransactionAsync();

            IdTransaction = Guid.NewGuid();
            return IdTransaction;
        }

        public void Commit(Guid? idTransaction)
        {
            if (IdTransaction != idTransaction)
                return;

            Context.Database.CurrentTransaction.Commit();
            IdTransaction = null;
        }

        public void Rollback(Guid? idTransaction)
        {
            if (IdTransaction != idTransaction)
                return;

            Context.Database.CurrentTransaction.Rollback();
            IdTransaction = null;
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