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
                new Category(Guid.Parse("a66e285e-0cb5-45d2-998c-d2cb092fafc3"), "Material escolar"),
                new Category(Guid.Parse("47e1eb5b-1a6b-4b0f-a759-6a159680ffb2"), "Eletronicos"),
                new Category(Guid.Parse("f331d521-319d-4511-ba3e-73febdc635ec"), "Acessorios")
            );
        }
    }
}
