using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectRiskConfiguration : IEntityTypeConfiguration<ProjectRisk>
{
    private const string TableName = "ProjectRisks";
    public void Configure(EntityTypeBuilder<ProjectRisk> builder)
    {
        builder.MetaConfiguration<ProjectRisk, long>(TableName);

        builder.Property(oo => oo.Title)
            .HasComment(GlobalCmts.Title)
            .HasColumnType("nvarchar(250)")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(oo => oo.Code)
            .HasComment(GlobalCmts.Code)
            .HasColumnType("nvarchar(100)")
            .HasMaxLength(100);

        builder.Property(oo => oo.RiskProbability)
            .HasComment(ProjectCmts.RiskProbability)
            .IsRequired();

        builder.Property(oo => oo.RiskImpact)
            .HasComment(ProjectCmts.RiskImpact)
            .IsRequired();

        builder.Property(oo => oo.RiskStatus)
            .HasComment(GlobalCmts.Status)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectRisks)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}