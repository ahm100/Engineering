using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Persistence.Configurations.CostOvers;

public class EmployerOperationConfiguration : IEntityTypeConfiguration<EmployerOperation>
{
    private const string TableName = "EmployerOperations";
    public void Configure(EntityTypeBuilder<EmployerOperation> builder)
    {
        builder.MetaConfiguration<EmployerOperation, long>(TableName);

        builder.Property(oo => oo.TotalPrice)
            .HasComment(EContractCmts.TotalPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.IncreaseRate)
            .HasColumnType("decimal(5,2)")
            .HasComment(EContractCmts.IncreaseRate)
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasComment(GlobalCmts.Description)
            .HasMaxLength(1500);

        builder.HasOne(oo => oo.EmployerContract)
            .WithMany(oo => oo.EmployerOperations)
            .HasForeignKey("EmployerContractId")
            .HasPrincipalKey(nameof(EmployerContract.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectOperation)
            .WithMany(oo => oo.EmployerOperations)
            .HasForeignKey("ProjectOperationId")
            .HasPrincipalKey(nameof(ProjectOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}