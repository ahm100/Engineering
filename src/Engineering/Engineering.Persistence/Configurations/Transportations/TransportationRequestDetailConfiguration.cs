using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationRequestDetailConfiguration : IEntityTypeConfiguration<TransportationRequestDetail>
{
    private const string _tableName = "TransportationRequestDetails";
    public void Configure(EntityTypeBuilder<TransportationRequestDetail> builder)
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

        builder.Property(oo => oo.GlobalFreightNumber)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.ClassifiedFreightNumber)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.InsuranceNumber)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.OrderNumber)
            .HasColumnType("nvarchar(50)");

        builder.Property(oo => oo.Tax)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.TransferPrice)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.ServicePrice)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.InsurancePrice)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.ShippingCost)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.ProductTotalPrice)
            .HasColumnType("decimal(18, 2)");

        builder.Property(oo => oo.OutofRange)
            .HasColumnType("decimal(18, 2)");
    }
}
