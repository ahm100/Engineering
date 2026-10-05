using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Persistence.Configurations.RequestMachineries;

public class RequestMachineryProjectOperationConfiguration : IEntityTypeConfiguration<RequestMachineryProjectOperation>
{
    private const string _tableName = "RequestMachineryProjectOperations";
    public void Configure(EntityTypeBuilder<RequestMachineryProjectOperation> builder)
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


        builder.HasOne(oo => oo.ProjectOperation)
               .WithMany(oo => oo.RequestMachineryProjectOperations)
               .HasForeignKey("ProjectOperationId")
               .HasPrincipalKey(nameof(ProjectOperation.Id))
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oo => oo.RequestMachinery)
               .WithMany(oo => oo.ProjectOperations)
               .HasForeignKey("RequestMachineryId")
               .HasPrincipalKey(nameof(RequestMachinery.Id))
               .OnDelete(DeleteBehavior.Restrict);
    }
}
