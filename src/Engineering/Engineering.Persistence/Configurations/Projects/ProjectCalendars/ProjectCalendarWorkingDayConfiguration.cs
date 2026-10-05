using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Persistence.Configurations.Projects.ProjectCalendars;

public class ProjectCalendarWorkingTimeConfiguration : IEntityTypeConfiguration<ProjectCalendarWorkingTime>
{
    private const string TableName = "ProjectCalendarWorkingTimes";
    public void Configure(EntityTypeBuilder<ProjectCalendarWorkingTime> builder)
    {
        builder.MetaConfiguration<ProjectCalendarWorkingTime, long>(TableName);

        builder.Property(oo => oo.From)
            .HasComment(ProjectCmts.ExceptionFrom);

        builder.Property(oo => oo.To)
            .HasComment(ProjectCmts.ExceptionTo);

        builder.HasOne(oo => oo.ProjectCalendarWorkingDay)
            .WithMany(oo => oo.ProjectCalendarWorkingTimes)
            .HasForeignKey(oo => oo.ProjectCalendarWorkingDayId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}