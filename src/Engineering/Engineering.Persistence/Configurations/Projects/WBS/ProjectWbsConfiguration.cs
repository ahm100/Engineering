using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Persistence.Configurations.Projects.WBS;

public class ProjectWbsConfiguration : IEntityTypeConfiguration<ProjectWbs>
{
    private const string TableName = "ProjectWbses";
    public void Configure(EntityTypeBuilder<ProjectWbs> builder)
    {
        builder.MetaConfiguration<ProjectWbs, long>(TableName);

        builder.Property(oo => oo.TitleFa)
            .HasComment(GlobalCmts.TitleFa)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.TitleEn)
            .HasComment(GlobalCmts.TitleEn)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.Code)
            .HasComment(GlobalCmts.Code)
            .HasColumnType("nvarchar(250)");

        builder.Property(oo => oo.DescriptionFa)
            .HasComment(GlobalCmts.DescriptionFa)
            .HasColumnType("nvarchar(1500)");

        builder.Property(oo => oo.DescriptionEn)
            .HasComment(GlobalCmts.DescriptionEn)
            .HasColumnType("nvarchar(1500)");

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectWbses)
            .HasForeignKey(oo => oo.ProjectId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(r => r.Parent)
            .WithMany(r => r.Childs)
            .HasForeignKey(r => r.ParentId)
            .HasPrincipalKey(r => r.Id)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(oo => oo.ProjectScheduleImport)
            .WithMany(oo => oo.ProjectWbses)
            .HasForeignKey(oo => oo.ProjectScheduleImportId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}