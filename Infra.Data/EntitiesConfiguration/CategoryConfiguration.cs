using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new Category(Guid.NewGuid(), "Material escolar"),
                new Category(Guid.NewGuid(), "Eletronicos"),
                new Category(Guid.NewGuid(), "Acessorios")
            );
        }
    }
}
