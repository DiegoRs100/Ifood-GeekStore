using System;
using System.Threading.Tasks;

namespace GeekStore.Core.Interfaces.BuildingBlocks
{
    public interface IContextServiceBase :  IDisposable
    {
        Task CreateTransaction();
        void Commit();
        void Rollback();
    }
}