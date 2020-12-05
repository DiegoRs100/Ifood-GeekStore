using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeekStore.Core.Base;
using GeekStore.Core.Dto_s;

namespace GeekStore.Data.Mappings
{
    internal class ImagemMapping : MappingBase<Imagem>, IEntityTypeConfiguration<Imagem>
    {
        public void Configure(EntityTypeBuilder<Imagem> builder)
        {
            builder.ToTable("TB_IMAGENS");
            builder.HasKey(x => x.Id);

            ConfigureProperties(builder);
        }

        protected override void ConfigureProperties(EntityTypeBuilder<Imagem> builder)
        {
            builder.Property(x => x.Path)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Extensao)
                .HasColumnType("VARCHAR(5)");
        }
    }
}