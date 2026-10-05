using Engineering.Domain.Entities.Branchs;
using Engineering.Domain.Entities.Categories;

namespace Engineering.Persistence.Configurations.Branchs;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    private const string TableName = "EngineeringBranchs";
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.MetaActiveConfiguration<Branch, long>(TableName);

        builder.Property(oo => oo.BranchName)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.BranchCode)
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.PreferentialReferenceCode)
            .HasComment("کد مرجع تفصیلی")
            .IsRequired();

        builder.HasOne(oo => oo.Category)
            .WithMany(oo => oo.Branchs)
            .HasForeignKey("CategoryId").HasPrincipalKey(nameof(Category.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}