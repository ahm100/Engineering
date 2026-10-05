using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractStatusHistoryConfiguration : IEntityTypeConfiguration<ContractStatusHistory>
{
    private const string TableName = "ContractStatusHistories";

    public void Configure(EntityTypeBuilder<ContractStatusHistory> builder)
    {
        builder.MetaConfiguration<ContractStatusHistory, long>(TableName);

        builder.Property(oo => oo.ContractId)
            .HasComment(GlobalCmts.ContractId)
            .IsRequired();
        builder.Property(oo => oo.FromStatus)
            .HasComment(ContractCmts.FromStatus)
            .IsRequired();
        builder.Property(oo => oo.ToStatus)
            .HasComment(ContractCmts.ToStatus)
            .IsRequired();
        builder.Property(oo => oo.TransitionType)
            .HasComment(ContractCmts.TransitionType)
            .IsRequired();
        builder.Property(oo => oo.EffectiveDate)
            .HasComment(ContractCmts.EffectiveDate)
            .IsRequired();
        builder.Property(oo => oo.Reason)
            .HasComment(ContractCmts.Reason);
        builder.Property(oo => oo.Description)
            .HasComment(ContractCmts.Description);
        builder.Property(oo => oo.SuspensionDurationMonths)
            .HasComment(ContractCmts.SuspensionDurationMonths);

        builder.HasOne(oo => oo.Contract)
            .WithMany(oo => oo.StatusHistories)
            .HasForeignKey(oo => oo.ContractId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(oo => oo.Documents)
            .WithOne(oo => oo.ContractStatusHistory)
            .HasForeignKey(oo => oo.ContractStatusHistoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
