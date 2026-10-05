using Engineering.Application.Services.Projects.Models.ProjectWarehouseModels;
using Engineering.Domain.Entities.Projects;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetsById;

namespace Engineering.Application.Services.Projects;

partial class ProjectLogic
{
    private async Task<Result<List<ProjectWarehouseRequest>?>> ValidateProjectWarehouseSelection(
        List<ProjectWarehouseRequest?>? projectWarehouses,
        CT ct)
    {
        if (projectWarehouses is null)
            return Result.Success<List<ProjectWarehouseRequest>?>(null);

        var normalizedProjectWarehouses = projectWarehouses.ToList();
        var warehouseIds = normalizedProjectWarehouses.Listed(oo => oo.Id);

        var warehouseResult = await _mediator.Send(
            new GetsWarehouseByIdQuery(
                1,
                warehouseIds.Count,
                warehouseIds.Select(oo => (long?)oo).ToList()),
            ct);

        if (warehouseResult.IsBad() || warehouseResult.Value?.Data?.Count != warehouseIds.Count)
            return Result.Failure<List<ProjectWarehouseRequest>?>(ProjectWarehouseErrors.WarehouseNotFound);

        return Result.Success<List<ProjectWarehouseRequest>?>(normalizedProjectWarehouses);
    }

    private async Task<Result<bool>> SynchronizeProjectWarehouses(
        Project project,
        List<ProjectWarehouseRequest?>? projectWarehouses,
        CT ct)
    {
        if (projectWarehouses is null)
            return true;

        var warehouseSelection = await ValidateProjectWarehouseSelection(projectWarehouses, ct);
        if (warehouseSelection.IsBad())
            return warehouseSelection.Failure<bool>()!;

        var desiredProjectWarehouses = warehouseSelection.Value!;
        var desiredByWarehouseId = desiredProjectWarehouses.ToDictionary(oo => oo.Id);
        var currentProjectWarehouses = await _projectWarehouseRepository.GetProjectWarehouses(project.Id, ct);
        var currentWarehouseIds = currentProjectWarehouses.Listed(oo => oo.WarehouseId);

        foreach (var projectWarehouse in currentProjectWarehouses)
        {
            if (!desiredByWarehouseId.TryGetValue(projectWarehouse.WarehouseId, out var desiredProjectWarehouse))
            {
                projectWarehouse.Remove();
                continue;
            }

            projectWarehouse.ChangeDefault(desiredProjectWarehouse.IsDefault);
        }

        foreach (var desiredProjectWarehouse in desiredProjectWarehouses.Where(
                     oo => !currentWarehouseIds.Contains(oo.Id)))
            project.AddWarehouse(desiredProjectWarehouse.Id, desiredProjectWarehouse.IsDefault);

        return true;
    }
}
