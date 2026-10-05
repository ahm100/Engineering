using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Configurations.RequestMachineries;

public class RequestMachineryBillConfiguration : IEntityTypeConfiguration<RequestMachineryBill>
{
    private const string _tableName = "RequestMachineryBills";
    public void Configure(EntityTypeBuilder<RequestMachineryBill> builder)
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
        .ValueGeneratedOnAdd().
        IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.ContractorId);

        builder.Property(oo => oo.SupplierId);

        builder.Property(oo => oo.TotalPrice);

        builder.Property(oo => oo.UnitPrice);

        builder.Property(oo => oo.BillNumber)
            .IsRequired(false)
            .HasDefaultValueSql("NEXT VALUE FOR engineer.RequestMachinaryBill_BillNumber");

        builder.Property(oo => oo.BillDate)
            .IsRequired();

        builder.Property(oo => oo.DriverId);

        builder.Property(oo => oo.BillConfirmerId);

        builder.Property(oo => oo.OperationDuration);

        builder.Property(oo => oo.FromDate);

        builder.Property(oo => oo.ToDate);

        builder.Property(oo => oo.NumberPlate);

        builder.Property(oo => oo.MachineryAssignment);

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.QRCodeUrl)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.DriverName)
            .HasColumnType("nvarchar(500)");

        builder.HasOne(oo => oo.RequestMachinery)
               .WithMany(oo => oo.RequestMachineryBills)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
