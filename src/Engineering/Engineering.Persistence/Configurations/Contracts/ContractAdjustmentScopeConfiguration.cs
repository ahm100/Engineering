using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractAdjustmentScopeConfiguration : IEntityTypeConfiguration<ContractAdjustmentScope>
{
    public void Configure(EntityTypeBuilder<ContractAdjustmentScope> builder)
    {
        builder.MetaConfiguration<ContractAdjustmentScope, long>("ContractAdjustmentScopes");
        builder.Property(x => x.ContractAdjustmentConfigurationId).IsRequired();
        builder.Property(x => x.ScopeType).IsRequired();
        builder.HasIndex(x => x.ContractAdjustmentConfigurationId);
        builder.HasOne(x => x.ContractAdjustmentConfiguration).WithMany(x => x.Scopes)
            .HasForeignKey(x => x.ContractAdjustmentConfigurationId)
            .OnDelete(DeleteBehavior.Cascade).IsRequired();
        builder.HasOne<ContractTypeDetail>().WithMany().HasForeignKey(x => x.ContractTypeDetailId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.ToTable("ContractAdjustmentScopes", table => table.HasCheckConstraint(
            "CK_ContractAdjustmentScopes_Shape",
            "([ScopeType] = 1 AND [ContractTypeKind] IS NULL AND [ContractTypeDetailId] IS NULL) OR " +
            "([ScopeType] = 2 AND [ContractTypeKind] IS NOT NULL AND [ContractTypeDetailId] IS NULL) OR " +
            "([ScopeType] = 3 AND [ContractTypeKind] IS NULL AND [ContractTypeDetailId] IS NOT NULL)"));
    }
}
