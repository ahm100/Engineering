using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    private const string _tableName = "Projects";
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ProjectName)
            .HasComment(ProjectCmts.ProjectName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.ProjectEnName)
            .HasComment(ProjectCmts.ProjectName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.ProjectCode)
            .HasComment(ProjectCmts.ProjectCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(oo => oo.Prefix)
            .HasComment(ProjectCmts.Prefix)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(oo => oo.EmployerId)
            .HasComment(ProjectCmts.EmployerId);

        builder.Property(oo => oo.CompanyId)
             .HasComment(GlobalCmts.CompanyId);

        builder.Property(oo => oo.SupervisorEngineer)
            .HasComment(ProjectCmts.SupervisorEngineer);

        builder.Property(oo => oo.Advisor)
            .HasComment(ProjectCmts.Advisor);

        builder.Property(oo => oo.ProjectManager)
            .HasComment(ProjectCmts.ProjectManager);

        builder.Property(oo => oo.PlanningAssistant)
            .HasComment(ProjectCmts.PlanningAssistant);

        builder.Property(oo => oo.AddAutomated)
            .HasComment(ProjectCmts.AddAutomated)
            .IsRequired();

        builder.Property(oo => oo.HasProduct)
            .HasComment(ProjectCmts.HasProduct)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Contractual)
            .HasComment(ProjectCmts.Contractual)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.ApprovedBudget)
            .HasComment(ProjectCmts.ApprovedBudget)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasComment(ProjectCmts.Status)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.CityId)
             .HasComment(GlobalCmts.CityId);

        builder.Property(oo => oo.Description)
             .HasComment(GlobalCmts.Description)
             .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.DescriptionEn)
             .HasComment(GlobalCmts.Description)
             .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.AddressDescription)
            .HasComment(ProjectCmts.AddressDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.PreferentialReferenceCode)
            .HasComment("کد مرجع تفصیلی")
            .IsRequired();

        builder.Property(oo => oo.OrganizationId)
            .HasComment(ProjectCmts.OrganizationId);

        builder.Property(oo => oo.IsOrganizationUnit)
            .HasComment(ProjectCmts.IsOrganizationUnit)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(oo => oo.ProjectType)
            .WithMany(oo => oo.Projects)
            .HasForeignKey("ProjectTypesId").HasPrincipalKey(nameof(ProjectType.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
