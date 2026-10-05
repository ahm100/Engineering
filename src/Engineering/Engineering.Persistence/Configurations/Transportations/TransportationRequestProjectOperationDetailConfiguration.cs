using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationRequestProjectOperationDetailConfiguration : IEntityTypeConfiguration<TransportationRequestProjectOperationDetail>
{
    private const string _tableName = "TransportationRequestProjectOperationDetails";
    public void Configure(EntityTypeBuilder<TransportationRequestProjectOperationDetail> builder)
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
            .WithMany(oo => oo.TransportationRequestProjectOperationDetails)
            .HasForeignKey("TransportationRequestId").HasPrincipalKey(nameof(TransportationRequest.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.TransportationRequestProjectOperationDetails)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
