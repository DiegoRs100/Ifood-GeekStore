using GeekStore.Core.Base;
using GeekStore.Core.Interfaces.BuildingBlocks;
using System;
using GeekStore.Business.Dto_s;
using GeekStore.Business.Interfaces.Services;
using GeekStore.Business.Dto_s.Validations;
using GeekStore.Business.Interfaces.Repositories;

namespace GeekStore.Business.Services
{
    public class ProdutoService : ServiceBase<Produto, ProdutoValidation>, IProdutoService
    {
        #region Injection

        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService (IProdutoRepository produtoRepository,
                               INotificationService notificador) : base(notificador, produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _produtoRepository?.Dispose();
        }
    }
}