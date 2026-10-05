using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Persistence.Configurations.Projects.ProjectCalendars;

public class ProjectCalendarWorkingDayConfiguration : IEntityTypeConfiguration<ProjectCalendarWorkingDay>
{
    private const string TableName = "ProjectCalendarWorkingDaies";
    public void Configure(EntityTypeBuilder<ProjectCalendarWorkingDay> builder)
    {
        builder.MetaConfiguration<ProjectCalendarWorkingDay, long>(TableName);

        builder.Property(oo => oo.DayOfWeek)
            .HasComment(ProjectCmts.DayOfWeek);

        builder.Property(oo => oo.IsWorking)
            .HasComment(ProjectCmts.IsWorking);

        builder.HasOne(oo => oo.ProjectCalendar)
            .WithMany(oo => oo.ProjectCalendarWorkingDaies)
            .HasForeignKey(oo => oo.ProjectCalendarId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

    }
}