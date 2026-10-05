using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractAdjustmentIndexConfiguration
    : IEntityTypeConfiguration<ContractAdjustmentIndex>
{
    private const string TableName = "ContractAdjustmentIndexes";

    public void Configure(
        EntityTypeBuilder<ContractAdjustmentIndex> builder)
    {
        builder.MetaConfiguration<
            ContractAdjustmentIndex,
            long>(TableName);

        builder.Property(oo => oo.ContractAdjustmentReferenceId)
            .IsRequired()
            .HasComment(ContractCmts.ContractAdjustmentReferenceId);

        builder.Property(oo => oo.Code)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100)
            .IsRequired()
            .HasComment(GlobalCmts.Code);

        builder.Property(oo => oo.FaTitle)
            .HasMaxLength(250)
            .IsRequired()
            .HasComment(GlobalCmts.FaTitle);

        builder.Property(oo => oo.EnTitle)
            .HasMaxLength(250)
            .IsRequired()
            .HasComment(GlobalCmts.EnTitle);

        builder.Property(oo => oo.Description)
            .HasMaxLength(1500)
            .HasComment(GlobalCmts.Description);

        builder.Property(oo => oo.IsActive)
            .IsRequired()
            .HasComment(GlobalCmts.IsActive);

        builder.HasIndex(oo => new
        {
            oo.ContractAdjustmentReferenceId,
            oo.Code
        })
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");
    }
}
