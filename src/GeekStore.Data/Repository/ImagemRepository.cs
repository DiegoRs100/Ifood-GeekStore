using GeekStore.Core.Base;
using GeekStore.Core.Dto_s;
using GeekStore.Core.Interfaces.Repositories;
using GeekStore.Data.Contexts;

namespace GeekStore.Data.Repository
{
    public class ImagemRepository : RepositoryBase<Imagem, GeekStoreDbContext>, IImagemRepository
    {
        public ImagemRepository(GeekStoreDbContext context) : base(context)
        { }
    }
}