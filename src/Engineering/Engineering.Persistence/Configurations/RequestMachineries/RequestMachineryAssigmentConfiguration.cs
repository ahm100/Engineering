using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Configurations.RequestMachineries;

public class RequestMachineryAssigmentConfiguration : IEntityTypeConfiguration<RequestMachineryAssignment>
{
    private const string _tableName = "RequestMachineryAssignments";
    public void Configure(EntityTypeBuilder<RequestMachineryAssignment> builder)
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

        builder.Property(oo => oo.MachineryIdentifier)
            .IsRequired();


        builder.HasOne(oo => oo.RequestMachinery)
               .WithMany(oo => oo.RequestMachineryAssignments)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);

    }
}
