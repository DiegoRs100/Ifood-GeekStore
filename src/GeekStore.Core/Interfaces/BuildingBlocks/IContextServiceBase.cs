using System;
using System.Threading.Tasks;

namespace GeekStore.Core.Interfaces.BuildingBlocks
{
    public interface IContextServiceBase :  IDisposable
    {
        Task<Guid?> CreateTransaction();
        void Commit(Guid? idTransaction);
        void Rollback(Guid? idTransaction);
    }
}