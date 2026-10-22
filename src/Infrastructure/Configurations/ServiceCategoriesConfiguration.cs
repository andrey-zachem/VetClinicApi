using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ServiceCategoriesConfiguration : IEntityTypeConfiguration<ServiceCategories>
{
    public void Configure(EntityTypeBuilder<ServiceCategories> builder)
    {
        builder.ToTable("service_categories");
        builder.HasKey(x => x.CategoryId);
        builder.Property(x => x.CategoryId)
            .HasColumnName("category_id")
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
