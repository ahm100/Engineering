using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.EmployerContracts;

public class EmployerOperationHistoryConfiguration : IEntityTypeConfiguration<EmployerOperationHistory>
{
    private const string TableName = "EmployerOperationHistories";

    public void Configure(EntityTypeBuilder<EmployerOperationHistory> builder)
    {
        builder.MetaConfiguration<EmployerOperationHistory, long>(TableName);

        builder.Property(oo => oo.Workload)
            .HasComment(EContractCmts.Workload)
            .HasColumnType("decimal(18,5)")
            .IsRequired();

        builder.Property(oo => oo.IncreaseRate)
            .HasColumnType("decimal(5,2)")
            .HasComment(EContractCmts.IncreaseRate)
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.UnitPrice)
            .HasComment(EContractCmts.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.EmployerOperation)
            .WithMany(oo => oo.EmployerOperationHistories)
            .HasForeignKey("EmployerOperationId").HasPrincipalKey(nameof(EmployerOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}