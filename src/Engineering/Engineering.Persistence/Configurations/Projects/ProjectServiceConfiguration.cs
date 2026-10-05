using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectServiceConfiguration : IEntityTypeConfiguration<ProjectService>
{
    private const string _tableName = "ProjectServices";
    public void Configure(EntityTypeBuilder<ProjectService> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.IsActive)
            .IsRequired();

        builder.Property(oo => oo.ContractorId)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectServices)
            .HasForeignKey("ProjectId").HasPrincipalKey(nameof(Project.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.ServiceInfo)
            .WithMany(oo => oo.ProjectServices)
            .HasForeignKey("ServiceInfoId").HasPrincipalKey(nameof(ServiceInfo.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}