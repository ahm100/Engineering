using Engineering.Application.Services.ProjectWarehouses.Contracts.CreateProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.SaveProjectWarehouses;
using Engineering.Application.Services.ProjectWarehouses.Contracts.UpdateProjectWarehouse;
using Engineering.Domain.Entities.Projects;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetById;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetsById;

namespace Engineering.Application.Services.ProjectWarehouses;

public partial class ProjectWarehouseLogic
{
    private async Task<Result<ProjectWarehouse?>> ExecuteCreateProjectWarehouse(
        CreateProjectWarehouseRequest request,
        CT ct)
    {
        try
        {
            if (!await _projectRepository.ExistsProject(request.ProjectId, ct))
                return Result.Failure<ProjectWarehouse>(ProjectWarehouseErrors.ProjectNotFound);

            if (await _projectWarehouseRepository.ExistsProjectWarehouse(
                    request.ProjectId, request.WarehouseId, null, ct))
                return Result.Failure<ProjectWarehouse>(ProjectWarehouseErrors.Duplicate);

            var warehouse = await _mediator.Send(new GetWarehouseByIdQuery(request.WarehouseId), ct);
            if (warehouse.IsBad() || warehouse.Value is null)
                return Result.Failure<ProjectWarehouse>(ProjectWarehouseErrors.WarehouseNotFound);

            var existing = await _projectWarehouseRepository.GetProjectWarehouses(request.ProjectId, ct);
            if (request.IsDefault)
            {
                var currentDefault = existing.FirstOrDefault(oo => oo.IsDefault);
                if (currentDefault is not null)
                {
                    currentDefault.ChangeDefault(false);
                    await _projectWarehouseRepository.UpdateProjectWarehouse(currentDefault, ct);
                }
            }

            var projectWarehouse = new ProjectWarehouse(request.ProjectId, request.WarehouseId, request.IsDefault);
            await _projectWarehouseRepository.CreateProjectWarehouse(projectWarehouse, ct);
            return projectWarehouse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectWarehouse>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> ExecuteSaveProjectWarehouses(
        SaveProjectWarehousesRequest request,
        CT ct)
    {
        try
        {
            if (!await _projectRepository.ExistsProject(request.ProjectId, ct))
                return Result.Failure<bool>(ProjectWarehouseErrors.ProjectNotFound);

            var retained = request.ProjectWarehouses.Where(oo => oo.IsDeleted != true).ToList();
            if (retained.Count(oo => oo.IsDefault) > 1)
                return Result.Failure<bool>(ProjectWarehouseErrors.MoreThanOneDefault);
            if (retained.All(oo => !oo.IsDefault))
                return Result.Failure<bool>(ProjectWarehouseErrors.NoDefault);

            var warehouseIds = retained.Listed(oo => oo.WarehouseId);
            var warehouseResult = await _mediator.Send(
                new GetsWarehouseByIdQuery(1, warehouseIds.Count, warehouseIds.Select(oo => (long?)oo).ToList()),
                ct);
            if (warehouseResult.IsBad() || warehouseResult.Value?.Data?.Count != warehouseIds.Count)
                return Result.Failure<bool>(ProjectWarehouseErrors.WarehouseNotFound);

            var existing = await _projectWarehouseRepository.GetProjectWarehouses(request.ProjectId, ct);
            var existingById = existing.ToDictionary(oo => oo.Id);
            var finalWarehouseIds = existing.ToDictionary(oo => oo.Id, oo => oo.WarehouseId);
            var newWarehouseIds = new List<long>();

            foreach (var item in request.ProjectWarehouses)
            {
                if (item.Id is > 0 && item.IsDeleted == true)
                    finalWarehouseIds.Remove(item.Id.Value);
                else if (item.Id is > 0)
                    finalWarehouseIds[item.Id.Value] = item.WarehouseId;
                else if (item.IsDeleted != true)
                    newWarehouseIds.Add(item.WarehouseId);
            }

            var resultingWarehouseIds = finalWarehouseIds.Values.Concat(newWarehouseIds).ToList();
            if (resultingWarehouseIds.Count != resultingWarehouseIds.Distinct().Count())
                return Result.Failure<bool>(ProjectWarehouseErrors.Duplicate);

            foreach (var item in request.ProjectWarehouses)
            {
                if (item.Id is > 0)
                {
                    if (!existingById.TryGetValue(item.Id.Value, out var projectWarehouse))
                        return Result.Failure<bool>(ProjectWarehouseErrors.NotFound);

                    if (item.IsDeleted == true)
                    {
                        projectWarehouse.Remove();
                        await _projectWarehouseRepository.UpdateProjectWarehouse(projectWarehouse, ct);
                        continue;
                    }

                    projectWarehouse.Update(item.WarehouseId, item.IsDefault);
                    await _projectWarehouseRepository.UpdateProjectWarehouse(projectWarehouse, ct);
                    continue;
                }

                if (item.IsDeleted == true)
                    continue;

                if (existing.Any(oo => oo.WarehouseId == item.WarehouseId))
                    return Result.Failure<bool>(ProjectWarehouseErrors.Duplicate);

                var projectWarehouseToCreate = new ProjectWarehouse(
                    request.ProjectId,
                    item.WarehouseId,
                    item.IsDefault);
                await _projectWarehouseRepository.CreateProjectWarehouse(projectWarehouseToCreate, ct);
            }

            var requestedDefault = retained.Single(oo => oo.IsDefault);
            foreach (var currentDefault in existing.Where(oo => oo.IsDefault))
            {
                if (requestedDefault.Id == currentDefault.Id)
                    continue;

                currentDefault.ChangeDefault(false);
                await _projectWarehouseRepository.UpdateProjectWarehouse(currentDefault, ct);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<ProjectWarehouse?>> ExecuteUpdateProjectWarehouse(
        UpdateProjectWarehouseRequest request,
        CT ct)
    {
        try
        {
            var projectWarehouse = await _projectWarehouseRepository.GetProjectWarehouse(request.Id, ct);
            if (projectWarehouse is null)
                return Result.Failure<ProjectWarehouse>(ProjectWarehouseErrors.NotFound);

            var warehouse = await _mediator.Send(new GetWarehouseByIdQuery(request.WarehouseId), ct);
            if (warehouse.IsBad() || warehouse.Value is null)
                return Result.Failure<ProjectWarehouse>(ProjectWarehouseErrors.WarehouseNotFound);

            if (await _projectWarehouseRepository.ExistsProjectWarehouse(
                    projectWarehouse.ProjectId, request.WarehouseId, request.Id, ct))
                return Result.Failure<ProjectWarehouse>(ProjectWarehouseErrors.Duplicate);

            var currentDefault = projectWarehouse.Project.ProjectWarehouses.FirstOrDefault(oo => oo.IsDefault);
            if (!request.IsDefault && projectWarehouse.IsDefault && projectWarehouse.Project.ProjectWarehouses.Count == 1)
                return Result.Failure<ProjectWarehouse>(ProjectWarehouseErrors.DefaultCannotBeUnset);

            if (request.IsDefault && currentDefault is not null && currentDefault.Id != projectWarehouse.Id)
            {
                currentDefault.ChangeDefault(false);
                await _projectWarehouseRepository.UpdateProjectWarehouse(currentDefault, ct);
            }

            projectWarehouse.Update(request.WarehouseId, request.IsDefault);
            await _projectWarehouseRepository.UpdateProjectWarehouse(projectWarehouse, ct);
            return projectWarehouse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectWarehouse>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<ProjectWarehouse?>> ExecuteDeleteProjectWarehouse(
        long id,
        CT ct)
    {
        try
        {
            var projectWarehouse = await _projectWarehouseRepository.GetProjectWarehouse(id, ct);
            if (projectWarehouse is null)
                return Result.Failure<ProjectWarehouse>(ProjectWarehouseErrors.NotFound);
            if (projectWarehouse.IsDefault)
                return Result.Failure<ProjectWarehouse>(ProjectWarehouseErrors.DefaultCannotBeDeleted);

            projectWarehouse.Remove();
            await _projectWarehouseRepository.UpdateProjectWarehouse(projectWarehouse, ct);
            return projectWarehouse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectWarehouse>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> ExecuteDeleteProjectWarehouses(
        List<long> ids,
        CT ct)
    {
        try
        {
            var projectWarehouses = await _projectWarehouseRepository.GetProjectWarehousesByIds(ids, ct);
            if (projectWarehouses.Count != ids.Count)
                return Result.Failure<bool>(ProjectWarehouseErrors.NotFound);
            if (projectWarehouses.Any(oo => oo.IsDefault))
                return Result.Failure<bool>(ProjectWarehouseErrors.DefaultCannotBeDeleted);

            foreach (var projectWarehouse in projectWarehouses)
            {
                projectWarehouse.Remove();
                await _projectWarehouseRepository.UpdateProjectWarehouse(projectWarehouse, ct);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
