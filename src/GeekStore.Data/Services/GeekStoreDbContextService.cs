using GeekStore.Core.Base;
using GeekStore.Core.Interfaces.Services.Contexts;
using GeekStore.Data.Contexts;

namespace GeekStore.Data.Services
{
    public class GeekStoreDbContextService : ContextServiceBase<GeekStoreDbContext>, IGeekStoreDbContextService
    {
        protected GeekStoreDbContextService(GeekStoreDbContext context) : base(context)
        { }
    }
}