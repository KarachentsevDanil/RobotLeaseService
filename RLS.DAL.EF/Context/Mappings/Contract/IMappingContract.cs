using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RLS.DAL.EF.Context.Mappings.Contract
{
    public interface IMappingContract<T>
        where T : class
    {
        void MapEntity(EntityTypeBuilder<T> builder);
    }
}