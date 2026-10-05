using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Configurations.Projects.WBS;

public class ProjectScheduleImportConfiguration : IEntityTypeConfiguration<ProjectScheduleImport>
{
    private const string TableName = "ProjectScheduleImports";
    public void Configure(EntityTypeBuilder<ProjectScheduleImport> builder)
    {
        builder.MetaConfiguration<ProjectScheduleImport, long>(TableName);

        builder.Property(oo => oo.FileName)
            .HasComment(GlobalCmts.TitleFa)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.Status)
            .HasComment(WbsCmts.ProjectScheduleImportStatus);

        builder.Property(oo => oo.ImportedAt)
            .HasComment(WbsCmts.ImportedAt);

        builder.Property(oo => oo.ImportedBy)
            .HasComment(WbsCmts.ImportedBy);

        builder.Property(oo => oo.ErrorMessage)
            .HasComment(WbsCmts.ErrorMessage)
            .HasColumnType("nvarchar(2500)");

        builder.Property(oo => oo.StatusDate)
            .HasComment(WbsCmts.StatusDate);

        builder.Property(oo => oo.RescheduleFromDate)
            .HasComment(WbsCmts.RescheduleFromDate);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectScheduleImports)
            .HasForeignKey(oo => oo.ProjectId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}