using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.ProjectUsers;

namespace Engineering.Persistence.Configurations.Projects;

public class ImplementationAssistantConfiguration : IEntityTypeConfiguration<ProjectImplementationAssistant>
{
    private const string _tableName = "ImplementationAssistants";
    public void Configure(EntityTypeBuilder<ProjectImplementationAssistant> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.ImplementationAssistantUserId)
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
            .WithMany(oo => oo.ProjectImplementationAssistants)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

    }
}