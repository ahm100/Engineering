using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Configurations.Projects.WBS;

public class ProjectScheduleColumnConfiguration : IEntityTypeConfiguration<ProjectScheduleColumn>
{
    private const string TableName = "ProjectScheduleColumns";

    public void Configure(EntityTypeBuilder<ProjectScheduleColumn> builder)
    {
        builder.MetaActiveConfiguration<ProjectScheduleColumn, long>(TableName);

        builder.Property(x => x.ColumnType)
            .HasComment(WbsCmts.ColumnType)
            .IsRequired();

        builder.Property(x => x.TitleFa)
            .HasComment(GlobalCmts.Title)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.TitleEn)
            .HasComment(GlobalCmts.TitleEn)
            .HasMaxLength(250);

        builder.Property(x => x.SortOrder)
            .HasComment(WbsCmts.SortOrder);

        builder.Property(x => x.DataType)
            .HasComment(WbsCmts.DataType)
            .IsRequired();

        builder.HasOne(x => x.ProjectScheduleImport)
            .WithMany(x => x.ProjectScheduleColumns)
            .HasForeignKey(x => x.ProjectScheduleImportId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.Navigation(x => x.ProjectScheduleTaskValues)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}