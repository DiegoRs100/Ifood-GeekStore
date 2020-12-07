using GeekStore.Business.Dto_s;
using GeekStore.Core.Interfaces.BuildingBlocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeekStore.Business.Interfaces.Services
{
    public interface IProdutoService : IServiceBase<Produto>, IDisposable
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid id);
    }
}