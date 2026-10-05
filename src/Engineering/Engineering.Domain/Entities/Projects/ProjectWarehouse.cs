namespace Engineering.Domain.Entities.Projects;

[Description(ProjectWarehouseCmts.ProjectWarehouse)]
public class ProjectWarehouse : AuditableEntity<ProjectWarehouse, long>
{
    [Description(GlobalCmts.ProjectId)]
    public long ProjectId { get; private set; }

    [Description(GlobalCmts.WarehouseId)]
    public long WarehouseId { get; private set; }

    [Description(ProjectWarehouseCmts.IsDefault)]
    public bool IsDefault { get; private set; }

    public Project Project { get; private set; }

    public ProjectWarehouse(
        long projectId,
        long warehouseId,
        bool isDefault) : this()
    {
        SetProjectId(projectId);
        SetWarehouseId(warehouseId);
        SetIsDefault(isDefault);
    }

    public ProjectWarehouse(
        Project project,
        long warehouseId,
        bool isDefault) : this()
    {
        SetProject(project);
        SetWarehouseId(warehouseId);
        SetIsDefault(isDefault);
    }

    public void Update(
        long warehouseId,
        bool isDefault)
    {
        SetWarehouseId(warehouseId);
        SetIsDefault(isDefault);
    }

    public void ChangeDefault(bool isDefault)
    {
        SetIsDefault(isDefault);
    }

    public void Remove()
    {
        SoftDelete();
    }

    private void SetProjectId(long projectId)
    {
        ProjectId = Guard.Against.NegativeOrZero(projectId, nameof(projectId));
    }

    private void SetProject(Project project)
    {
        Project = Guard.Against.Null(project, nameof(project));
    }

    private void SetWarehouseId(long warehouseId)
    {
        WarehouseId = Guard.Against.NegativeOrZero(warehouseId, nameof(warehouseId));
    }

    private void SetIsDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }

#pragma warning disable CS8618
    private ProjectWarehouse()
    {
    }
#pragma warning restore CS8618
}
