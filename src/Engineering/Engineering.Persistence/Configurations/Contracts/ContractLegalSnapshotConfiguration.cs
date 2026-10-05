using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractLegalSnapshotConfiguration : IEntityTypeConfiguration<ContractLegalSnapshot>
{
    private const string TableName = "ContractLegalSnapshots";

    public void Configure(EntityTypeBuilder<ContractLegalSnapshot> builder)
    {
        builder.MetaConfiguration<ContractLegalSnapshot, long>(TableName);

        builder.Property(oo => oo.ContractId)
            .HasComment(GlobalCmts.ContractId)
            .IsRequired();
        builder.Property(oo => oo.ProjectId)
            .HasComment(GlobalCmts.ProjectId)
            .IsRequired();
        builder.Property(oo => oo.ContractPartyId)
            .HasComment(ContractCmts.ContractPartyId)
            .IsRequired();
        builder.Property(oo => oo.FaTitle)
            .HasComment(GlobalCmts.FaTitle)
            .HasMaxLength(250)
            .IsRequired();
        builder.Property(oo => oo.EnTitle)
            .HasComment(GlobalCmts.EnTitle)
            .HasMaxLength(250)
            .IsRequired(false);
        builder.Property(oo => oo.Description)
            .HasComment(GlobalCmts.Description)
            .HasMaxLength(1500);
        builder.Property(oo => oo.StartDate)
            .HasComment(GlobalCmts.StartDate)
            .IsRequired();
        builder.Property(oo => oo.Duration)
            .HasComment(ContractCmts.Duration)
            .IsRequired();
        builder.Property(oo => oo.DurationUnit)
            .HasComment(ContractCmts.DurationUnit)
            .IsRequired();
        builder.Property(oo => oo.EndDate)
            .HasComment(GlobalCmts.EndDate)
            .IsRequired();
        builder.Property(oo => oo.Status)
            .HasComment(GlobalCmts.Status)
            .IsRequired();
        builder.Property(oo => oo.CurrencyId)
            .HasComment(GlobalCmts.CurrencyId)
            .IsRequired(false);
        builder.Property(oo => oo.InitialAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.InitialAmount)
            .IsRequired();
        builder.Property(oo => oo.FinalAmount)
            .HasColumnType("decimal(18,2)")
            .HasComment(ContractCmts.FinalContractAmount)
            .IsRequired();

        builder.HasOne(oo => oo.Contract)
            .WithOne(oo => oo.LegalSnapshot)
            .HasForeignKey<ContractLegalSnapshot>(oo => oo.ContractId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(oo => oo.ProjectId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
