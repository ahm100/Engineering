using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class FiduciaryProductDetailHistoryConfiguration : IEntityTypeConfiguration<FiduciaryProductDetailHistory>
{
    private const string _tableName = "FiduciaryProductDetailHistories";
    public void Configure(EntityTypeBuilder<FiduciaryProductDetailHistory> builder)
    {
        builder.ToTable(_tableName);

        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd().
            IsRequired();

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

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.ConfirmedLoanDays)
            .HasDefaultValue(null);

        builder.Property(oo => oo.ConfirmedDailyLateFine)
            .HasColumnType("decimal(18,2)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.LastDescription)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.StatusDescription)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.Property(oo => oo.Status)
            .IsRequired();


        builder.HasOne(oo => oo.FiduciaryProductDetail)
               .WithMany(oo => oo.Histories)
               .HasForeignKey("FiduciaryProductDetailId")
               .HasPrincipalKey(nameof(FiduciaryProductDetail.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}

