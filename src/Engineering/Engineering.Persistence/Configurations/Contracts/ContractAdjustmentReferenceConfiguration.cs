using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Configurations.Contracts;

public class ContractAdjustmentReferenceConfiguration
    : IEntityTypeConfiguration<ContractAdjustmentReference>
{
    private const string TableName = "ContractAdjustmentReferences";

    public void Configure(
        EntityTypeBuilder<ContractAdjustmentReference> builder)
    {
        builder.MetaConfiguration<ContractAdjustmentReference, long>(TableName);

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

        builder.HasIndex(oo => oo.Code)
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.HasMany(oo => oo.Indexes)
            .WithOne(oo => oo.ContractAdjustmentReference)
            .HasForeignKey(oo => oo.ContractAdjustmentReferenceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
