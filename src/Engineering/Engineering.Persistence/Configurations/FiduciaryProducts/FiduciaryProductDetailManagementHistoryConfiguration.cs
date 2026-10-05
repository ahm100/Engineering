using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Configurations.FiduciaryProducts;

public class FiduciaryProductDetailManagementHistoryConfiguration : IEntityTypeConfiguration<FiduciaryProductDetailManagementHistory>
{
    private const string _tableName = "FiduciaryProductDetailManagementHistories";
    public void Configure(EntityTypeBuilder<FiduciaryProductDetailManagementHistory> builder)
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

        builder.Property(oo => oo.WarehouseId)
            .IsRequired();

        builder.Property(oo => oo.InvoiceId)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .IsRequired();

        builder.Property(oo => oo.ConfirmedLoanCount)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.HasOne(oo => oo.FiduciaryProductDetailManagement)
               .WithMany(oo => oo.Histories)
               .HasForeignKey("FiduciaryProductDetailManagementId")
               .HasPrincipalKey(nameof(FiduciaryProductDetailManagement.Id))
               .OnDelete(DeleteBehavior.NoAction);
    }
}
