using Engineering.Domain.Entities.Projects;
using ContractEntity = Engineering.Domain.Entities.Contracts.Contract;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractConfiguration : IEntityTypeConfiguration<ContractEntity>
{
    private const string TableName = "Contracts";

    public void Configure(EntityTypeBuilder<ContractEntity> builder)
    {
        builder.MetaConfiguration<ContractEntity, long>(TableName);

        builder.Property(oo => oo.ContractNumber)
            .HasComment(ContractCmts.ContractNumber)
            .IsRequired(false)
            .HasDefaultValueSql("NEXT VALUE FOR engineer.Contract_ContractNumber");

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

        builder.Property(oo => oo.ProjectId)
            .HasComment(GlobalCmts.ProjectId)
            .IsRequired();

        builder.Property(oo => oo.ContractPartyId)
            .HasComment(ContractCmts.ContractPartyId)
            .IsRequired();

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

        builder.Property(oo => oo.IsRegistrationPending)
            .IsRequired();

        builder.Property(oo => oo.AdjustmentMethod)
            .IsRequired(false);

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId)
            .IsRequired();

        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(oo => oo.ProjectId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasIndex(oo => oo.ContractNumber)
            .IsUnique()
            .HasFilter("[ContractNumber] IS NOT NULL");
    }
}
