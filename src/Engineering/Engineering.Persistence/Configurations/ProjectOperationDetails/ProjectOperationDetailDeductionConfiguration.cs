using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Persistence.Configurations.ProjectOperationDetails;

public class ProjectOperationDetailDeductionConfiguration : IEntityTypeConfiguration<ProjectOperationDetailDeduction>
{
    private const string _tableName = "ProjectOperationDetailDeductions";
    public void Configure(EntityTypeBuilder<ProjectOperationDetailDeduction> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Length)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Width)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Height)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Weight)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Number)
            .HasColumnType("decimal(18,5)")
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.HasOne(oo => oo.ProjectOperationDetail)
            .WithMany(oo => oo.ProjectOperationDetailDeductions)
            .HasForeignKey("ProjectOperationDetailId").HasPrincipalKey(nameof(ProjectOperationDetail.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}