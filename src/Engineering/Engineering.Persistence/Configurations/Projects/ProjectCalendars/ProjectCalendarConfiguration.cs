using Engineering.Domain.Entities.Projects.ProjectCalendars;

namespace Engineering.Persistence.Configurations.Projects.ProjectCalendars;

public class ProjectCalendarConfiguration : IEntityTypeConfiguration<ProjectCalendar>
{
    private const string TableName = "ProjectCalendars";
    public void Configure(EntityTypeBuilder<ProjectCalendar> builder)
    {
        builder.MetaConfiguration<ProjectCalendar, long>(TableName);

        builder.Property(oo => oo.TitleFa)
            .HasComment(GlobalCmts.TitleFa)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.TitleEn)
            .HasComment(GlobalCmts.TitleEn)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.MppUid)
            .HasComment(ProjectCmts.ProjectCalendarMppUid);

        builder.Property(oo => oo.IsDefault)
            .HasComment(ProjectCmts.ProjectCalendarIsDefault);

        builder.Property(oo => oo.MinutesPerDay)
            .HasComment(ProjectCmts.ProjectCalendarMinutesPerDay);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectCalendars)
            .HasForeignKey(oo => oo.ProjectId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

    }
}