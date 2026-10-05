using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractChangeItemConfiguration
    : IEntityTypeConfiguration<ContractChangeItem>
{
    private const string TableName = "ContractChangeItems";

    public void Configure(EntityTypeBuilder<ContractChangeItem> builder)
    {
        builder.MetaConfiguration<ContractChangeItem, long>(TableName);

        builder.Property(oo => oo.ContractChangeId).IsRequired();
        builder.Property(oo => oo.PricingMethod)
            .HasComment(GlobalCmts.PricingMethod)
            .IsRequired();
        builder.Property(oo => oo.PreviousValue)
            .HasColumnType("decimal(23,5)")
            .HasComment(ContractCmts.PreviousValue)
            .IsRequired();
        builder.Property(oo => oo.NewValue)
            .HasColumnType("decimal(23,5)")
            .HasComment(ContractCmts.NewValue)
            .IsRequired();
        builder.Property(oo => oo.UnitPrice)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.UnitPrice);
        builder.Property(oo => oo.ChangeAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.ChangeAmount)
            .IsRequired();

        builder.HasIndex(oo => new { oo.ContractChangeId, oo.ContractTypeDetailId })
            .IsUnique()
            .HasFilter("[IsDeleted] = 0 AND [ContractTypeDetailId] IS NOT NULL");

        builder.HasIndex(oo => new
        {
            oo.ContractChangeId,
            oo.ContractTypeId,
            oo.ProjectOperationDetailId
        })
            .IsUnique()
            .HasFilter(
                "[IsDeleted] = 0 AND [ContractTypeId] IS NOT NULL AND [ProjectOperationDetailId] IS NOT NULL");

        builder.HasOne(oo => oo.ContractChange)
            .WithMany(oo => oo.Items)
            .HasForeignKey(oo => oo.ContractChangeId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(oo => oo.ContractTypeDetail)
            .WithMany()
            .HasForeignKey(oo => oo.ContractTypeDetailId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ContractType)
            .WithMany()
            .HasForeignKey(oo => oo.ContractTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany()
            .HasForeignKey(oo => oo.ProjectOperationDetailId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable(TableName, tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "CK_ContractChangeItems_ExactlyOneOrigin",
                "([ContractTypeDetailId] IS NOT NULL AND [ContractTypeId] IS NULL AND [ProjectOperationDetailId] IS NULL) OR " +
                "([ContractTypeDetailId] IS NULL AND [ContractTypeId] IS NOT NULL AND [ProjectOperationDetailId] IS NOT NULL)");
        });
    }
}
