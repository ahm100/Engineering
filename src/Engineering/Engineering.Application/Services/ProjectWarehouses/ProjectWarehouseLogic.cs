using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectWarehouses.Contracts;
using Engineering.Application.Services.ProjectWarehouses.Contracts.CreateProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.DeleteProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.DeleteProjectWarehouses;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetDefaultProjectWarehouse;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseAssets;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseById;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseInventory;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectId;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectIds;
using Engineering.Application.Services.ProjectWarehouses.Contracts.SaveProjectWarehouses;
using Engineering.Application.Services.ProjectWarehouses.Contracts.UpdateProjectWarehouse;
using Engineering.Application.WebServices.WarehouseServices.Inventories.Queries.GetProductInventoryByFilter;

namespace Engineering.Application.Services.ProjectWarehouses;

public partial class ProjectWarehouseLogic : IProjectWarehouseLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProjectWarehouseLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectWarehouseRepository _projectWarehouseRepository;
    private readonly IViewProductRepository _productRepository;

    public ProjectWarehouseLogic(
        IMediator mediator,
        ILogger<ProjectWarehouseLogic> logger,
        IUnitOfWork unitOfWork,
        IProjectRepository projectRepository,
        IProjectWarehouseRepository projectWarehouseRepository,
        IViewProductRepository productRepository)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _projectRepository = projectRepository;
        _projectWarehouseRepository = projectWarehouseRepository;
        _productRepository = productRepository;
    }

    public async Task<Result<CreateProjectWarehouseResponse?>> CreateProjectWarehouse(
        CreateProjectWarehouseRequest request,
        CT ct)
    {
        _logger.LogInformation("CreateProjectWarehouse");

        var validation = await request.IsValidAsync<CreateProjectWarehouseValidator, CreateProjectWarehouseRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<CreateProjectWarehouseResponse>(validation.Error!);

        var result = await ExecuteCreateProjectWarehouse(request, ct);
        if (result.IsBad())
            return result.Failure<CreateProjectWarehouseResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateProjectWarehouseResponse(result.Value!.Id, true);
    }

    public async Task<Result<SaveProjectWarehousesResponse?>> SaveProjectWarehouses(
        SaveProjectWarehousesRequest request,
        CT ct)
    {
        _logger.LogInformation("SaveProjectWarehouses");

        var validation = await request.IsValidAsync<SaveProjectWarehousesValidator, SaveProjectWarehousesRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<SaveProjectWarehousesResponse>(validation.Error!);

        var result = await ExecuteSaveProjectWarehouses(request, ct);
        if (result.IsBad())
            return result.Failure<SaveProjectWarehousesResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new SaveProjectWarehousesResponse(true);
    }

    public async Task<Result<UpdateProjectWarehouseResponse?>> UpdateProjectWarehouse(
        UpdateProjectWarehouseRequest request,
        CT ct)
    {
        _logger.LogInformation("UpdateProjectWarehouse");

        var validation = await request.IsValidAsync<UpdateProjectWarehouseValidator, UpdateProjectWarehouseRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<UpdateProjectWarehouseResponse>(validation.Error!);

        var result = await ExecuteUpdateProjectWarehouse(request, ct);
        if (result.IsBad())
            return result.Failure<UpdateProjectWarehouseResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateProjectWarehouseResponse(true);
    }

    public async Task<Result<DeleteProjectWarehouseResponse?>> DeleteProjectWarehouse(
        DeleteProjectWarehouseRequest request,
        CT ct)
    {
        _logger.LogInformation("DeleteProjectWarehouse");

        var validation = await request.IsValidAsync<DeleteProjectWarehouseValidator, DeleteProjectWarehouseRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<DeleteProjectWarehouseResponse>(validation.Error!);

        var result = await ExecuteDeleteProjectWarehouse(request.Id, ct);
        if (result.IsBad())
            return result.Failure<DeleteProjectWarehouseResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectWarehouseResponse(true);
    }

    public async Task<Result<DeleteProjectWarehousesResponse?>> DeleteProjectWarehouses(
        DeleteProjectWarehousesRequest request,
        CT ct)
    {
        _logger.LogInformation("DeleteProjectWarehouses");

        var validation = await request.IsValidAsync<DeleteProjectWarehousesValidator, DeleteProjectWarehousesRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<DeleteProjectWarehousesResponse>(validation.Error!);

        var result = await ExecuteDeleteProjectWarehouses(request.Ids, ct);
        if (result.IsBad())
            return result.Failure<DeleteProjectWarehousesResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteProjectWarehousesResponse(true);
    }

    public async Task<Result<GetProjectWarehouseByIdResponse?>> GetProjectWarehouseById(
        GetProjectWarehouseByIdRequest request,
        CT ct)
    {
        _logger.LogInformation("GetProjectWarehouseById");

        var validation = await request.IsValidAsync<GetProjectWarehouseByIdValidator, GetProjectWarehouseByIdRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetProjectWarehouseByIdResponse>(validation.Error!);

        var value = await _projectWarehouseRepository.GetProjectWarehouseById(request.Id, ct);
        if (value is null)
            return Result.Failure<GetProjectWarehouseByIdResponse>(ProjectWarehouseErrors.NotFound);

        var warehouse = await WebServicesLogic.WarehouseDataReceiver(value.WarehouseId, _mediator, ct);
        if (warehouse is null)
            return Result.Failure<GetProjectWarehouseByIdResponse>(ProjectWarehouseErrors.WarehouseNotFound);

        return new GetProjectWarehouseByIdResponse(
            value.ProjectWarehouseId,
            value.ProjectId,
            value.WarehouseId,
            warehouse.WarehouseTypeId,
            warehouse.ManagerId,
            warehouse.Contact,
            warehouse.ManagerFullName,
            warehouse.Code,
            warehouse.Name,
            value.IsDefault);
    }

    public async Task<Result<GetProjectWarehousesByProjectIdResponse?>> GetProjectWarehousesByProjectId(
        GetProjectWarehousesByProjectIdRequest request,
        CT ct)
    {
        _logger.LogInformation("GetProjectWarehousesByProjectId");

        var validation = await request.IsValidAsync<GetProjectWarehousesByProjectIdValidator, GetProjectWarehousesByProjectIdRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetProjectWarehousesByProjectIdResponse>(validation.Error!);

        if (!await _projectRepository.ExistsProject(request.ProjectId, ct))
            return Result.Failure<GetProjectWarehousesByProjectIdResponse>(ProjectWarehouseErrors.ProjectNotFound);

        var result = await _projectWarehouseRepository.GetProjectWarehousesByProjectId(
            request.ProjectId,
            request.PageIndex,
            request.PageSize,
            ct);

        if (result.RowCount == 0)
            return Result.Failure<GetProjectWarehousesByProjectIdResponse>(ProjectWarehouseErrors.DataNotFound);

        if (!await EnrichWarehouses(result.Data, ct))
            return Result.Failure<GetProjectWarehousesByProjectIdResponse>(ProjectWarehouseErrors.WarehouseNotFound);
        return new GetProjectWarehousesByProjectIdResponse(result.Data, result.RowCount);
    }

    public async Task<Result<GetDefaultProjectWarehouseResponse?>> GetDefaultProjectWarehouse(
        GetDefaultProjectWarehouseRequest request,
        CT ct)
    {
        _logger.LogInformation("GetDefaultProjectWarehouse");

        var validation = await request.IsValidAsync<GetDefaultProjectWarehouseValidator, GetDefaultProjectWarehouseRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetDefaultProjectWarehouseResponse>(validation.Error!);

        if (!await _projectRepository.ExistsProject(request.ProjectId, ct))
            return Result.Failure<GetDefaultProjectWarehouseResponse>(ProjectWarehouseErrors.ProjectNotFound);

        var value = await _projectWarehouseRepository.GetDefaultProjectWarehouseByProjectId(request.ProjectId, ct);
        if (value is null)
            return Result.Failure<GetDefaultProjectWarehouseResponse>(ProjectWarehouseErrors.DataNotFound);

        if (!await EnrichWarehouses([value], ct))
            return Result.Failure<GetDefaultProjectWarehouseResponse>(ProjectWarehouseErrors.WarehouseNotFound);
        return new GetDefaultProjectWarehouseResponse(value);
    }

    public async Task<Result<GetProjectWarehousesByProjectIdsResponse?>> GetProjectWarehousesByProjectIds(
        GetProjectWarehousesByProjectIdsRequest request,
        CT ct)
    {
        _logger.LogInformation("GetProjectWarehousesByProjectIds");

        var validation = await request.IsValidAsync<GetProjectWarehousesByProjectIdsValidator, GetProjectWarehousesByProjectIdsRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetProjectWarehousesByProjectIdsResponse>(validation.Error!);

        if (!await _projectRepository.ExistsProjects(request.ProjectIds, ct))
            return Result.Failure<GetProjectWarehousesByProjectIdsResponse>(ProjectWarehouseErrors.ProjectNotFound);

        var result = await _projectWarehouseRepository.GetProjectWarehousesByProjectIds(
            request.ProjectIds,
            request.PageIndex,
            request.PageSize,
            ct);
        if (result.RowCount == 0)
            return Result.Failure<GetProjectWarehousesByProjectIdsResponse>(ProjectWarehouseErrors.DataNotFound);

        var warehouses = result.Data.Select(oo => new ProjectWarehouseModel
        {
            ProjectWarehouseId = oo.ProjectWarehouseId,
            WarehouseId = oo.WarehouseId,
            IsDefault = oo.IsDefault
        }).ToList();
        if (!await EnrichWarehouses(warehouses, ct))
            return Result.Failure<GetProjectWarehousesByProjectIdsResponse>(ProjectWarehouseErrors.WarehouseNotFound);

        var warehousesById = warehouses.ToDictionary(oo => oo.ProjectWarehouseId);
        var data = result.Data
            .GroupBy(oo => new { oo.ProjectId, oo.ProjectCode, oo.ProjectName })
            .Select(oo => new GetProjectWarehousesByProjectIdsModel(
                oo.Key.ProjectId,
                oo.Key.ProjectCode,
                oo.Key.ProjectName,
                oo.Select(xx => warehousesById[xx.ProjectWarehouseId]).ToList()))
            .ToList();

        return new GetProjectWarehousesByProjectIdsResponse(data, result.RowCount);
    }

    public async Task<Result<GetProjectWarehouseInventoryResponse?>> GetProjectWarehouseInventory(
        GetProjectWarehouseInventoryRequest request,
        CT ct)
    {
        _logger.LogInformation("GetProjectWarehouseInventory");

        var validation = await request.IsValidAsync<GetProjectWarehouseInventoryValidator, GetProjectWarehouseInventoryRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetProjectWarehouseInventoryResponse>(validation.Error!);

        if (!await _projectRepository.ExistsProject(request.ProjectId, ct))
            return Result.Failure<GetProjectWarehouseInventoryResponse>(ProjectWarehouseErrors.ProjectNotFound);

        var associations = await _projectWarehouseRepository.GetProjectWarehousesByProjectId(
            request.ProjectId, request.PageIndex, request.PageSize, ct);
        if (associations.RowCount == 0)
            return Result.Failure<GetProjectWarehouseInventoryResponse>(ProjectWarehouseErrors.DataNotFound);

        var warehouseIds = associations.Data.Listed(oo => oo.WarehouseId);
        var inventory = await _mediator.Send(
            new GetProductInventoryByFilterQuery(request.ProductId, warehouseIds, request.FilterData, 0, 0),
            ct);
        if (inventory.IsBad() || inventory.Value?.Data is null)
            return Result.Failure<GetProjectWarehouseInventoryResponse>(ProjectWarehouseErrors.WarehouseNotFound);

        var data = inventory.Value.Data.Select(oo =>
        {
            var association = associations.Data.FirstOrDefault(xx => xx.WarehouseId == oo.WareHouseId);
            return new GetProjectWarehouseInventoryModel
            {
                WarehouseId = oo.WareHouseId,
                WarehouseCode = oo.WareHouseCode,
                WarehouseName = association?.IsDefault == true ? $"{oo.WareHouseName}(پیش فرض)" : oo.WareHouseName,
                InStockCount = oo.RealQuantity,
                RequestQuantity = oo.RequestQuantity,
                IsDefault = association?.IsDefault == true
            };
        }).ToList();

        return new GetProjectWarehouseInventoryResponse(data, associations.RowCount);
    }

    public async Task<Result<GetProjectWarehouseAssetsResponse?>> GetProjectWarehouseAssets(
        GetProjectWarehouseAssetsRequest request,
        CT ct)
    {
        _logger.LogInformation("GetProjectWarehouseAssets");

        var validation = await request.IsValidAsync<GetProjectWarehouseAssetsValidator, GetProjectWarehouseAssetsRequest>(ct);
        if (validation.IsFailure)
            return Result.Failure<GetProjectWarehouseAssetsResponse>(validation.Error!);

        if (!await _projectRepository.ExistsProject(request.ProjectId, ct))
            return Result.Failure<GetProjectWarehouseAssetsResponse>(ProjectWarehouseErrors.ProjectNotFound);

        var associations = await _projectWarehouseRepository.GetProjectWarehousesByProjectId(
            request.ProjectId, request.PageIndex, request.PageSize, ct);
        if (associations.RowCount == 0)
            return Result.Failure<GetProjectWarehouseAssetsResponse>(ProjectWarehouseErrors.DataNotFound);

        return await GetProjectWarehouseAssets(request, associations.Data, associations.RowCount, ct);
    }
}
