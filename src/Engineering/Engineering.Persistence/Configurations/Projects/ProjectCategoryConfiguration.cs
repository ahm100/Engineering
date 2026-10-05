using Engineering.Domain.Entities.Categories;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Junctions;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectCategoryConfiguration : IEntityTypeConfiguration<ProjectCategory>
{
    private const string TableName = "ProjectCategories";
    public void Configure(EntityTypeBuilder<ProjectCategory> builder)
    {
        builder.MetaConfiguration<ProjectCategory, long>(TableName);

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectCategories)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.Category)
            .WithMany(oo => oo.ProjectCategories)
            .HasForeignKey("CategoryId").HasPrincipalKey(nameof(Category.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}
