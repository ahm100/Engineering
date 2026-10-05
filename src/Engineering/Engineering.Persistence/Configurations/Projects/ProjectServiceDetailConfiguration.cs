using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectServiceDetailConfiguration : IEntityTypeConfiguration<ProjectServiceDetail>
{
    private const string _tableName = "ProjectServiceDetails";
    public void Configure(EntityTypeBuilder<ProjectServiceDetail> builder)
    {
        builder.ToTable(_tableName);
        builder.HasKey(oo => oo.Id);

        builder.Property(oo => oo.Id)
            .UseIdentityColumn(2L, 1)
            .IsRequired();

        builder.Property(oo => oo.Created)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(oo => oo.CreatorId)
            .IsRequired();

        builder.Property(oo => oo.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(oo => oo.ProjectService)
            .WithMany(oo => oo.ProjectServiceDetails)
            .HasForeignKey("ProjectServiceId").HasPrincipalKey(nameof(ProjectService.Id))
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(oo => oo.OperationInfoService)
            .WithMany(oo => oo.ProjectServiceDetails)
            .HasForeignKey("OperationInfoServiceId").HasPrincipalKey(nameof(OperationInfoService.Id))
            .OnDelete(DeleteBehavior.NoAction);
    }
}