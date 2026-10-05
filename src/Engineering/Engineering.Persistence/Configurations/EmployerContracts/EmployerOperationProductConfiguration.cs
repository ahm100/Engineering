using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.EmployerContracts;

public class EmployerOperationProductConfiguration : IEntityTypeConfiguration<EmployerOperationProduct>
{
    private const string TableName = "EmployerOperationProducts";
    public void Configure(EntityTypeBuilder<EmployerOperationProduct> builder)
    {
        builder.MetaConfiguration<EmployerOperationProduct, long>(TableName);

        builder.Property(oo => oo.ProductGroupId)
            .HasComment(EContractCmts.ProductGroupId)
            .IsRequired();

        builder.Property(oo => oo.ProductId)
            .HasComment(EContractCmts.ProductId);

        builder.Property(oo => oo.Count)
            .HasComment(EContractCmts.Count);

        builder.Property(oo => oo.MinPrice)
            .HasColumnType("decimal(18,2)")
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

        builder.HasOne(oo => oo.EmployerOperation)
            .WithMany(oo => oo.EmployerOperationProducts)
            .HasForeignKey("EmployerOperationId")
            .HasPrincipalKey(nameof(EmployerOperation.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}