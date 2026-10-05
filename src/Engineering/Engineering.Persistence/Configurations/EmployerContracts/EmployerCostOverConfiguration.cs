using Engineering.Domain.Entities.CostOvers;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.CostOvers;

public class EmployerCostOverConfiguration : IEntityTypeConfiguration<EmployerCostOver>
{
    private const string TableName = "EmployerCostOvers";
    public void Configure(EntityTypeBuilder<EmployerCostOver> builder)
    {
        builder.MetaConfiguration<EmployerCostOver, long>(TableName);

        builder.Property(oo => oo.Percent)
            .HasComment(EContractCmts.Percent)
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.HasOne(oo => oo.EmployerContract)
            .WithMany(oo => oo.EmployerCostOvers)
            .HasForeignKey("EmployerContractId")
            .HasPrincipalKey(nameof(EmployerContract.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.CostOver)
            .WithMany(oo => oo.EmployerCostOvers)
            .HasForeignKey("CostOverId")
            .HasPrincipalKey(nameof(CostOver.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}