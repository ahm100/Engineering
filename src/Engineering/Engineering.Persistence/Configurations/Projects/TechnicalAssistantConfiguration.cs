using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.ProjectUsers;

namespace Engineering.Persistence.Configurations.Projects;

public class TechnicalAssistantConfiguration : IEntityTypeConfiguration<ProjectTechnicalAssistant>
{
    private const string _tableName = "TechnicalAssistants";
    public void Configure(EntityTypeBuilder<ProjectTechnicalAssistant> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.TechnicalAssistantUserId)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();


        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectTechnicalAssistants)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}