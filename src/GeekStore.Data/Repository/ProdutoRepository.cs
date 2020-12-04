using GeekStore.Core.Base;
using GeekStore.Data.Contexts;
using GeekStore.Business.Dto_s;
using GeekStore.Business.Interfaces.Repositories;

namespace GeekStore.Data.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto, GeekStoreDbContext>, IProdutoRepository
    {
        public ProdutoRepository(GeekStoreDbContext context) : base(context)
        { }
    }
}