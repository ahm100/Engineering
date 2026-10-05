using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.EmployerContracts;

public class EmployerConsiderationDepConfiguration : IEntityTypeConfiguration<EmployerConsiderationDep>
{
    private const string TableName = "EmployerConsiderationDeps";
    public void Configure(EntityTypeBuilder<EmployerConsiderationDep> builder)
    {
        builder.MetaConfiguration<EmployerConsiderationDep, long>(TableName);

        builder.HasOne(oo => oo.EmployerConsideration)
            .WithMany(oo => oo.EmployerConsiderationDeps)
            .HasForeignKey("EmployerConsiderationId")
            .HasPrincipalKey(nameof(EmployerConsideration.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.EmployerOperation)
            .WithMany(oo => oo.EmployerConsiderationDeps)
            .HasForeignKey("EmployerOperationId")
            .HasPrincipalKey(nameof(EmployerOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}