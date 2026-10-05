using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.Projects.Histories;
namespace Engineering.Persistence.Configurations.Projects;

public class ProjectHistoryConfiguration : IEntityTypeConfiguration<ProjectHistory>
{
    private const string TableName = "ProjectHistories";
    public void Configure(EntityTypeBuilder<ProjectHistory> builder)
    {
        builder.MetaActiveConfiguration<ProjectHistory, long>(TableName);

        builder.Property(oo => oo.ProjectName)
            .HasComment(ProjectCmts.ProjectName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.ProjectCode)
            .HasComment(ProjectCmts.ProjectCode)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(oo => oo.Prefix)
            .HasComment(ProjectCmts.Prefix)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(oo => oo.ProjectEnName)
            .HasComment(ProjectCmts.ProjectName)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250);

        builder.Property(oo => oo.DescriptionEn)
             .HasComment(GlobalCmts.Description)
             .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.EmployerId)
            .HasComment(ProjectCmts.EmployerId);

        builder.Property(oo => oo.CompanyId)
            .HasComment(GlobalCmts.CompanyId);

        builder.Property(oo => oo.SupervisorEngineer)
            .HasComment(ProjectCmts.SupervisorEngineer);

        builder.Property(oo => oo.Advisor)
            .HasComment(ProjectCmts.Advisor);

        builder.Property(oo => oo.ProjectManager)
            .HasComment(ProjectCmts.ProjectManager)
            .IsRequired();

        builder.Property(oo => oo.PlanningAssistant)
            .HasComment(ProjectCmts.PlanningAssistant);

        builder.Property(oo => oo.AddAutomated)
            .HasComment(ProjectCmts.AddAutomated)
            .IsRequired();

        builder.Property(oo => oo.CityId)
             .HasComment(GlobalCmts.CityId);

        builder.Property(oo => oo.Description)
             .HasComment(GlobalCmts.Description)
             .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.AddressDescription)
            .HasComment(ProjectCmts.AddressDescription)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.ApprovedBudget)
            .HasComment(ProjectCmts.ApprovedBudget)
            .HasColumnType("decimal(18,2)");

        builder.Property(oo => oo.Contractual)
            .HasComment(ProjectCmts.Contractual)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(oo => oo.OrganizationId)
            .HasComment(ProjectCmts.OrganizationId);

        builder.Property(oo => oo.IsOrganizationUnit)
            .HasComment(ProjectCmts.IsOrganizationUnit)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Status)
            .HasComment(ProjectCmts.Status)
            .IsRequired();

        builder.Property(oo => oo.PreferentialReferenceCode)
            .HasComment(GlobalCmts.PreferentialReferenceCode)
            .HasComment("کد مرجع تفصیلی")
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectHistories)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(CostCenter.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
