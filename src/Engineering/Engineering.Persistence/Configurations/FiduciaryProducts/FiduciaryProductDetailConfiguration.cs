using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class FiduciaryProductDetailConfiguration : IEntityTypeConfiguration<FiduciaryProductDetail>
{
    private const string _tableName = "FiduciaryProductDetails";
    public void Configure(EntityTypeBuilder<FiduciaryProductDetail> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.ProductId)
            .IsRequired();

        builder.Property(oo => oo.MeasureUnitId)
            .IsRequired();

        builder.Property(oo => oo.CurrencyId)
            .IsRequired();

        builder.Property(oo => oo.LoanCount)
            .IsRequired();

        builder.Property(oo => oo.LoanDays)
            .IsRequired();

        builder.Property(oo => oo.DailyLateFine)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(oo => oo.ConfirmedLoanDays)
            .HasDefaultValue(null);

        builder.Property(oo => oo.ConfirmedDailyLateFine)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.DeliverDate)
            .HasDefaultValue(null);

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.StatusDescription)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.Status)
            .HasDefaultValue(FiduciaryProductDetailStatus.New)
            .IsRequired();

        builder.HasOne(oo => oo.FiduciaryProduct)
               .WithMany(oo => oo.Details)
               .HasForeignKey("FiduciaryProductId")
               .HasPrincipalKey(nameof(FiduciaryProduct.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
