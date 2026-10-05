using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Persistence.Configurations.CostOvers;

public class CostOverConfiguration : IEntityTypeConfiguration<CostOver>
{
    private const string _tableName = "CostOvers";
    public void Configure(EntityTypeBuilder<CostOver> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.CostOverName)
            .HasMaxLength(250)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.CostOverCode)
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.CompanyId);

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.ApprovalStatus)
            .HasComment(CostOverCmts.ApprovalStatus)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.CurrentApprovalAttemptId)
            .HasComment(CostOverCmts.CurrentApprovalAttemptId)
            .IsRequired(false);

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

    }
}
