using GeekStore.Business.Dto_s;
using GeekStore.Core.Dto_s;
using GeekStore.Core.Extentions;
using GeekStore.Core.Interfaces.BuildingBlocks;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GeekStore.Data.Contexts
{
    public class GeekStoreDbContext : DbContext
    {
        #region DbSet's

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }

        #endregion

        #region Injection

        private readonly ISessionApp _appSession;

        public GeekStoreDbContext(DbContextOptions<GeekStoreDbContext> options, ISessionApp appSession) : base(options)
        {
            _appSession = appSession;
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UpdateData(GetType());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.UpdateData(_appSession);
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}