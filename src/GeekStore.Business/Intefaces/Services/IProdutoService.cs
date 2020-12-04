using GeekStore.Business.Dto_s;
using GeekStore.Core.Interfaces.BuildingBlocks;
using System;

namespace GeekStore.Business.Interfaces.Services
{
    public interface IProdutoService : IServiceBase<Produto>, IDisposable
    { }
}