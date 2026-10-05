using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Persistence.Configurations.Transportations;

public class TransportationRequestProjectOperationConfiguration : IEntityTypeConfiguration<TransportationRequestProjectOperation>
{
    private const string _tableName = "TransportationRequestProjectOperations";
    public void Configure(EntityTypeBuilder<TransportationRequestProjectOperation> builder)
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
            .WithMany(oo => oo.TransportationRequestProjectOperations)
            .HasForeignKey("TransportationRequestId").HasPrincipalKey(nameof(TransportationRequest.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.TransportationRequestProjectOperations)
            .HasForeignKey("ProjectOperationId").HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}
