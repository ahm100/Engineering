using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationRequestCostCenterConfiguration : IEntityTypeConfiguration<TransportationRequestCostCenter>
{
    private const string _tableName = "TransportationRequestCostCenters";
    public void Configure(EntityTypeBuilder<TransportationRequestCostCenter> builder)
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

        builder.HasOne(oo => oo.TransportationRequest)
            .WithMany(oo => oo.TransportationRequestCostCenters)
            .HasForeignKey("TransportationRequestId").HasPrincipalKey(nameof(TransportationRequest.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.CostCenter)
            .WithMany(oo => oo.TransportationRequestCostCenters)
            .HasForeignKey("CostCenterId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
