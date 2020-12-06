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
            ConfigureRelationships(builder);
        }

        protected override void ConfigureProperties(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(x => x.Nome)
                .HasColumnType("VARCHAR(100)");
        }

        protected override void ConfigureRelationships(EntityTypeBuilder<Produto> builder)
        {
            builder.HasOne(x => x.Imagem)
                .WithOne()
                .HasForeignKey<Produto>(x => x.IdImagem);
        }
    }
}