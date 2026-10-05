using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.EmployerContracts;

public class EmployerOperationServicesHistoryConfiguration : IEntityTypeConfiguration<EmployerOperationServiceHistory>
{
    private const string TableName = "EmployerOperationServicesHistories";
    public void Configure(EntityTypeBuilder<EmployerOperationServiceHistory> builder)
    {
        builder.MetaConfiguration<EmployerOperationServiceHistory, long>(TableName);

        builder.Property(oo => oo.MinPrice)
            .HasComment(EContractCmts.MinPrice)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(oo => oo.MaxPrice)
            .HasColumnType("decimal(18,2)")
            .HasComment(EContractCmts.MaxPrice)
            .IsRequired();

        builder.Property(oo => oo.Tax)
            .HasColumnType("decimal(18,2)")
            .HasComment(EContractCmts.Tax)
            .IsRequired();

        builder.Property(oo => oo.TaxPercent)
            .HasColumnType("decimal(5,2)")
            .HasComment(EContractCmts.TaxPercent)
            .IsRequired();

        builder.Property(oo => oo.TransportationCost)
            .HasColumnType("decimal(18,2)")
            .HasComment(EContractCmts.TransportationCost)
            .IsRequired();

        builder.Property(oo => oo.TransportationCostPercent)
            .HasColumnType("decimal(5,2)")
            .HasComment(EContractCmts.TransportationCostPercent)
            .IsRequired();

        builder.Property(oo => oo.ProfitCost)
            .HasColumnType("decimal(18,2)")
            .HasComment(EContractCmts.ProfitCost)
            .IsRequired();

        builder.Property(oo => oo.ProfitCostPercent)
            .HasColumnType("decimal(5,2)")
            .HasComment(EContractCmts.ProfitCostPercent)
            .IsRequired();

        builder.Property(oo => oo.OtherCost)
            .HasColumnType("decimal(18,2)")
            .HasComment(EContractCmts.OtherCost)
            .IsRequired();

        builder.Property(oo => oo.OtherCostPercent)
            .HasColumnType("decimal(5,2)")
            .HasComment(EContractCmts.OtherCostPercent)
            .IsRequired();

        builder.Property(oo => oo.IsStandard)
            .HasDefaultValue(true)
            .HasComment(EContractCmts.IsStandard)
            .IsRequired();

        builder.Property(oo => oo.Description)
            .HasColumnType("nvarchar(1500)")
            .HasComment(GlobalCmts.Description);
    }
}