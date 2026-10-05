using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Persistence.Configurations.Projects.ProjectCalendars;

public class ProjectCalendarExceptionConfiguration : IEntityTypeConfiguration<ProjectCalendarException>
{
    private const string TableName = "ProjectCalendarExceptions";
    public void Configure(EntityTypeBuilder<ProjectCalendarException> builder)
    {
        builder.MetaConfiguration<ProjectCalendarException, long>(TableName);

        builder.Property(oo => oo.Date)
            .HasComment(ProjectCmts.ExceptionDate);

        builder.Property(oo => oo.IsWorking)
            .HasComment(ProjectCmts.ExceptionIsWorking);

        builder.Property(oo => oo.From)
            .HasComment(ProjectCmts.ExceptionFrom);

        builder.Property(oo => oo.To)
            .HasComment(ProjectCmts.ExceptionTo);

        builder.Property(oo => oo.Description)
            .HasComment(ProjectCmts.ExceptionDescription)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.ProjectCalendar)
            .WithMany(oo => oo.ProjectCalendarExceptions)
            .HasForeignKey(oo => oo.ProjectCalendarId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

    }
}