using GeekStore.Core.Dto_s;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Core.Base
{
    public abstract class MappingBase<TEntity> where TEntity : EntityBase
    {
        protected virtual void ConfigureProperties(EntityTypeBuilder<TEntity> builder)
        { }

        protected virtual void ConfigureIndexs(EntityTypeBuilder<TEntity> builder)
        { }

        protected virtual void ConfigureRelationships(EntityTypeBuilder<TEntity> builder)
        { }
    }
}