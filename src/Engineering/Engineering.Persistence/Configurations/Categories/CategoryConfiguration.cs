using Engineering.Domain.Entities.Categories;

namespace Engineering.Persistence.Configurations.Categories;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private const string TableName = "EngineeringCategories";
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.MetaActiveConfiguration<Category, long>(TableName);

        builder.Property(oo => oo.CategoryName)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.CategoryCode)
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.PreferentialReferenceCode)
            .HasComment("کد مرجع تفصیلی")
            .IsRequired();
    }
}