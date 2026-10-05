using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Configurations.CostOvers;

public class EmployerCostOverImpactConfiguration : IEntityTypeConfiguration<EmployerCostOverImpact>
{
    private const string TableName = "EmployerCostOverImpacts";
    public void Configure(EntityTypeBuilder<EmployerCostOverImpact> builder)
    {
        builder.MetaConfiguration<EmployerCostOverImpact, long>(TableName);

        builder.Property(oo => oo.Percent)
            .HasComment(EContractCmts.Percent)
            .HasColumnType("decimal(5,2)")
            .IsRequired();


        builder.HasOne(oo => oo.ParentCostOver)
            .WithMany(oo => oo.ParentCostOverImpacts)
            .HasForeignKey("ParentCostOverId")
            .HasPrincipalKey(nameof(EmployerCostOver.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ChildCostOver)
            .WithMany(oo => oo.ChildCostOverImpacts)
            .HasForeignKey("ChildCostOverId")
            .HasPrincipalKey(nameof(EmployerCostOver.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}