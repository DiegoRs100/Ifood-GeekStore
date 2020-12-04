using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeekStore.Core.Base;
using GeekStore.Business.Dto_s;

namespace GeekStore.Data.Mappings
{
    internal class ProdutoMapping : MappingBase<Produto>, IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("TB_PRODUTOS");
            builder.HasKey(x => x.Id);

            ConfigureProperties(builder);
        }

        protected override void ConfigureProperties(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(x => x.Descricao)
                .HasColumnType("VARCHAR(100)");
        }
    }
}