using GeekStore.Core.Base;
using GeekStore.Core.Interfaces.Services;
using GeekStore.Data.Contexts;

namespace GeekStore.Data.Services
{
    public class GeekStoreDbContextService : ContextServiceBase<GeekStoreDbContext>, IGeekStoreDbContextService
    {
        public GeekStoreDbContextService(GeekStoreDbContext context) : base(context)
        { }
    }
}