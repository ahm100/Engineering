using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Configurations.RequestGoodsSupplies;

public class RequestGoodsSupplyConfiguration : IEntityTypeConfiguration<RequestGoodsSupply>
{
    private const string _tableName = "RequestGoodsSupplies";
    public void Configure(EntityTypeBuilder<RequestGoodsSupply> builder)
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

        builder.Property(oo => oo.Importance);

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.SupplyerId);

        builder.Property(oo => oo.BuyerId);

        builder.Property(oo => oo.CurrencyId);

        builder.Property(oo => oo.RequestedDate);

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.ConsumptionRateAndInventoryUrl)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500);

        builder.Property(oo => oo.ConsumptionAddress)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500);

        builder.Property(oo => oo.DescriptionEn)
            .HasColumnType("nvarchar(1500)")
            .HasMaxLength(1500);

        builder.Property(oo => oo.DeliveryDeadline);

        builder.Property(oo => oo.RegistrationNumber)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.DeviceName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.DeviceEnName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.DeviceNumber)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.DeviceCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(oo => oo.UnitCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(oo => oo.RequestingOrganizationId);

        builder.Property(oo => oo.PurchaseLocation);

        builder.Property(oo => oo.PurchaseReason);

        builder.Property(oo => oo.Status)
            .HasDefaultValue(GoodsSupplyStatus.Created)
            .IsRequired();

        builder.Property(oo => oo.Type)
            .IsRequired();

        builder.Property(oo => oo.IsPettyCash);

        builder.Property(oo => oo.ServiceReasonType);

        builder.Property(oo => oo.IsArchived)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.TransferPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.OtherPrice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountOnInvoicePercentage)
             .HasColumnType("decimal(5, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountOnInvoiceNumber)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.DiscountedPriceOnInvoice)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TaxOnInvoicePercentage)
             .HasColumnType("decimal(5, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.TaxOnInvoiceNumber)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);

        builder.Property(oo => oo.FinalInvoiceAmount)
             .HasColumnType("decimal(18, 2)")
             .HasDefaultValue(null);


        builder.Property(oo => oo.SerialNumber)
            .HasColumnType("nvarchar(250)")
            .IsRequired();

        builder.Ignore(oo => oo.RequestSerialNumber);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasDefaultValue(null);

        builder.HasOne(oo => oo.ProjectOperation)
               .WithMany(oo => oo.RequestGoodsSupplies)
               .HasForeignKey("ProjectOperationId")
               .HasPrincipalKey(nameof(ProjectOperation.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.ProjectOperationDetail)
               .WithMany(oo => oo.RequestGoodsSupplies)
               .HasForeignKey("ProjectOperationDetailId")
               .HasPrincipalKey(nameof(ProjectOperationDetail.Id))
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);

        builder.HasOne(oo => oo.OperationInfoSeason)
               .WithMany(oo => oo.RequestGoodsSupplies)
               .HasForeignKey("OperationInfoSeasonId")
               .HasPrincipalKey(nameof(OperationInfoSeason.Id))
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);

        builder.HasOne(oo => oo.Project)
               .WithMany(oo => oo.RequestGoodsSupplies)
               .HasForeignKey("ProjectId")
               .HasPrincipalKey(nameof(Project.Id))
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);

        builder.HasOne(oo => oo.Parent)
               .WithMany(oo => oo.Childs)
               .HasForeignKey("ParentId")
               .HasPrincipalKey(nameof(RequestGoodsSupply.Id))
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);
    }
}
