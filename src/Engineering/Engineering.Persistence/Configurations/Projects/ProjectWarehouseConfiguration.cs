using Engineering.Domain.Entities.Projects;

namespace Engineering.Persistence.Configurations.Projects;

public class ProjectWarehouseConfiguration : IEntityTypeConfiguration<ProjectWarehouse>
{
    private const string TableName = "ProjectWarehouses";

    public void Configure(EntityTypeBuilder<ProjectWarehouse> builder)
    {
        builder.MetaConfiguration<ProjectWarehouse, long>(TableName);

        builder.Property(oo => oo.WarehouseId)
            .HasComment(GlobalCmts.WarehouseId)
            .IsRequired();

        builder.Property(oo => oo.IsDefault)
            .HasComment(ProjectWarehouseCmts.IsDefault)
            .HasDefaultValue(false)
            .IsRequired();

        builder.HasOne(oo => oo.Project)
            .WithMany(oo => oo.ProjectWarehouses)
            .HasForeignKey(oo => oo.ProjectId)
            .HasPrincipalKey(oo => oo.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
