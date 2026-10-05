using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithoutInclude;
using Engineering.Application.Services.CostCenters.Queries.GetCostCenterWithWarehousesInclude;
using Engineering.Application.Services.CostCenterWarehouses.Commands.Create;
using Engineering.Application.Services.CostCenterWarehouses.Commands.Delete;
using Engineering.Application.Services.CostCenterWarehouses.Commands.DeleteWarehouseFromCostCenter;
using Engineering.Application.Services.CostCenterWarehouses.Commands.Update;
using Engineering.Application.Services.CostCenterWarehouses.Models.CostCenterWarehouseGroupDelete;
using Engineering.Application.Services.CostCenterWarehouses.Models.Create;
using Engineering.Application.Services.CostCenterWarehouses.Models.Creates;
using Engineering.Application.Services.CostCenterWarehouses.Models.Delete;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetById;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetDefaultCostCenterId;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsByCostCenterId;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseAssets;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseByCostCenterIds;
using Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseInventory;
using Engineering.Application.Services.CostCenterWarehouses.Models.Models;
using Engineering.Application.Services.CostCenterWarehouses.Models.Update;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetById;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetDefaultByCostCenterId;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetsByCostCenterId;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetsCostCenterWarehouseByCostCenterIds;
using Engineering.Application.Services.CostCenterWarehouses.Queries.GetsCostCenterWarehouseInventory;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupId;
using Engineering.Application.WebServices.WarehouseServices.Inventories.Models.GetProductInventoryByFilter;
using Engineering.Application.WebServices.WarehouseServices.Inventories.Queries.GetProductInventoryByFilter;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetById;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetsById;

namespace Engineering.Application.Services.CostCenterWarehouses;

public class CostCenterWarehouseLogic : ICostCenterWarehouseLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<CostCenterWarehouseLogic> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IViewProductRepository _pRepo;

    public CostCenterWarehouseLogic(
        IMediator mediator,
        ILogger<CostCenterWarehouseLogic> logger,
        IUnitOfWork unitOfWork,
        IViewProductRepository pRepo)
    {
        _mediator = mediator;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _pRepo = pRepo;
    }

    public async Task<Result<CreateCostCenterWarehouseResponse?>> CreateCostCenterWarehouse(
        CreateCostCenterWarehouseRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreateCostCenterWarehouse, CostCenterId:{CostCenterId}, Id:{Id},", request.CostCenterId, request.Id);

        var isValidRequest = await request.IsValidAsync<CreateCostCenterWarehouseValidator, CreateCostCenterWarehouseRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreateCostCenterWarehouseResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithWarehousesIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<CreateCostCenterWarehouseResponse>(costCenter.Error!);
        if (costCenter.Value is null)
            return Result.Failure<CreateCostCenterWarehouseResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        if (costCenter.Value!.CostCenterWarehouses.Any(oo => oo.WarehouseId == request.Id))
            return Result.Failure<CreateCostCenterWarehouseResponse>(CostCenterWarehouseErrors.WarehouseIdIsDuplicate);

        var warehouseData = await _mediator.Send(new GetWarehouseByIdQuery(request.Id), ct);
        if (warehouseData.IsFailure)
            return Result.Failure<CreateCostCenterWarehouseResponse>(CostCenterWarehouseErrors.WarehouseNotFound);

        var response = await _mediator.Send(new CreateCostCenterWarehouseCommand(costCenter.Value!, request.Id, request.IsDefault), ct);
        if (response.IsFailure)
            return Result.Failure<CreateCostCenterWarehouseResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new CreateCostCenterWarehouseResponse(response.Value!.Id, true);
    }

    public async Task<Result<CreatesCostCenterWarehouseResponse?>> CreateCostCenterWarehouses(
        CreatesCostCenterWarehouseRequest request, CT ct)
    {
        _logger.LogInformation("Request for CreatesCostCenterWarehouse, CostCenterId:{CostCenterId}", request.CostCenterId);

        var isValidRequest = await request.IsValidAsync<CreatesCostCenterWarehouseValidator, CreatesCostCenterWarehouseRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CreatesCostCenterWarehouseResponse>(isValidRequest.Error!);

        var costCenterQuery = await _mediator.Send(new GetCostCenterWithWarehousesIncludeQuery(request.CostCenterId), ct);
        if (costCenterQuery.IsFailure || costCenterQuery.Value is null)
            return Result.Failure<CreatesCostCenterWarehouseResponse>(CostCenterErrors.CostCenterWithIdNotFound);
        var costCenter = costCenterQuery.Value;

        var warehouseIds = request.CostCenterWarehouses?.Where(x => x.Id == null).Select(x => x.WarehouseId).Distinct().ToList();
        if (warehouseIds != null && warehouseIds.Count > 0)
            if (costCenter.CostCenterWarehouses.Any(oo => warehouseIds.Contains(oo.WarehouseId)))
                return Result.Failure<CreatesCostCenterWarehouseResponse>(CostCenterWarehouseErrors.WarehouseIdIsDuplicate);

        var ids = request.CostCenterWarehouses?.Where(x => x.IsDeleted != true).Select(x => x.WarehouseId).Distinct().ToList();
        if (ids != null && ids.Count > 0)
        {
            var warehouseData = await _mediator.Send(new GetsWarehouseByIdQuery(1, ids.Count, ids.Adapt<List<long?>>().Distinct().ToList()), ct);
            if (warehouseData.IsFailure)
                return Result.Failure<CreatesCostCenterWarehouseResponse>(CostCenterWarehouseErrors.WarehouseNotFound);
        }

        if (request.CostCenterWarehouses is not null && request.CostCenterWarehouses.Count > 0)
        {
            if (request.CostCenterWarehouses.Where(x => x!.IsDefault).Count() > 1)
                return Result.Failure<CreatesCostCenterWarehouseResponse>(CostCenterErrors.CostCenterWarehousesHaveMoreDefault!);
            if (request.CostCenterWarehouses.Where(x => x!.IsDefault).Count() < 1)
                return Result.Failure<CreatesCostCenterWarehouseResponse>(CostCenterErrors.CostCenterWarehousesHaveNoDefault!);
            if (request.CostCenterWarehouses.Where(x => x!.IsDefault && x.IsDeleted != null && x.IsDeleted == true).Count() == 1)
                return Result.Failure<CreatesCostCenterWarehouseResponse>(CostCenterErrors.CostCenterDefaultWarehouseCantDelete!);

            var requestDefault = request.CostCenterWarehouses.Where(x => x!.IsDefault).FirstOrDefault();
            foreach (var item in request.CostCenterWarehouses)
            {
                if (item.Id != null && item.Id > 0 && item.IsDeleted == true)
                {
                    if (requestDefault!.Id == item.Id)
                        return Result.Failure<CreatesCostCenterWarehouseResponse>(CostCenterWarehouseErrors.IsDefaultWarehouse);

                    var deleteResponse = await _mediator.Send(new DeleteWarehouseFromCostCenterCommand((long)item.Id!), ct);
                    if (deleteResponse.IsFailure)
                        return Result.Failure<CreatesCostCenterWarehouseResponse>(deleteResponse.Error!);
                }
                else if (item.Id != null && item.Id > 0 && (item.IsDeleted == null || item.IsDeleted == false))
                {
                    var costCenterWarehouse = costCenter.CostCenterWarehouses.FirstOrDefault(x => x.Id == item.Id);

                    var response = await _mediator.Send(new UpdateCostCenterWarehouseCommand(item.Id.Value, item.WarehouseId, item.IsDefault), ct);
                    if (response.IsFailure)
                        return Result.Failure<CreatesCostCenterWarehouseResponse>(response.Error!);
                }
                else if (item.Id == null && (item.IsDeleted == null || item.IsDeleted == false))
                {
                    var response = await _mediator.Send(new CreateCostCenterWarehouseCommand(costCenter, item.WarehouseId, item.IsDefault), ct);
                    if (response.IsFailure)
                        return Result.Failure<CreatesCostCenterWarehouseResponse>(response.Error!);
                }
            }
        }

        await _unitOfWork.CommitAsync(ct);
        return new CreatesCostCenterWarehouseResponse(true);
    }

    public async Task<Result<UpdateCostCenterWarehouseResponse?>> UpdateCostCenterWarehouse(
        UpdateCostCenterWarehouseRequest request, CT ct)
    {
        _logger.LogInformation("Request for UpdateCostCenterWarehouse, CostCenterWarehouseId:{CostCenterWarehouseId}, Id:{Id},", request.CostCenterWarehouseId, request.Id);

        var isValidRequest = await request.IsValidAsync<UpdateCostCenterWarehouseValidator, UpdateCostCenterWarehouseRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<UpdateCostCenterWarehouseResponse>(isValidRequest.Error!);

        var warehouseData = await _mediator.Send(new GetWarehouseByIdQuery(request.Id), ct);
        if (warehouseData.IsFailure)
            return Result.Failure<UpdateCostCenterWarehouseResponse>(CostCenterWarehouseErrors.WarehouseNotFound);

        var response = await _mediator.Send(new UpdateCostCenterWarehouseCommand(request.CostCenterWarehouseId, request.Id, request.IsDefault), ct);
        if (response.IsFailure)
            return Result.Failure<UpdateCostCenterWarehouseResponse>(response.Error!);

        await _unitOfWork.CommitAsync(ct);
        return new UpdateCostCenterWarehouseResponse(response.Value!.Id, true);
    }

    public async Task<Result<DeleteCostCenterWarehouseResponse?>> DeleteCostCenterWarehouse(
        DeleteCostCenterWarehouseRequest request, CT ct)
    {
        _logger.LogInformation($"Request for DeleteCostCenterWarehouse, CostCenterWarehouseId: {request.CostCenterWarehouseId}.");

        var isValidRequest = await request.IsValidAsync<DeleteCostCenterWarehouseValidator, DeleteCostCenterWarehouseRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<DeleteCostCenterWarehouseResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new DeleteCostCenterWarehouseCommand(request.CostCenterWarehouseId), ct);
        if (response.IsFailure)
            return Result.Failure<DeleteCostCenterWarehouseResponse>(response.Error!);

        var value = response.Value!;
        await _unitOfWork.CommitAsync(ct);

        return new DeleteCostCenterWarehouseResponse(value.Id, true);
    }

    public async Task<Result<CostCenterWarehouseGroupDeleteResponse?>> CostCenterWarehouseGroupDelete(
        CostCenterWarehouseGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("Request for CostCenterWarehouseGroupDelete, Ids:{Ids}", request.Ids);

        var isValidRequest = await request.IsValidAsync<CostCenterWarehouseGroupDeleteValidator, CostCenterWarehouseGroupDeleteRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<CostCenterWarehouseGroupDeleteResponse>(isValidRequest.Error!);

        foreach (var item in request.Ids)
        {
            var response = await _mediator.Send(new DeleteCostCenterWarehouseCommand(item), ct);
            if (response.IsFailure)
                return Result.Failure<CostCenterWarehouseGroupDeleteResponse>(response.Error!);
        }

        await _unitOfWork.CommitAsync(ct);
        return new CostCenterWarehouseGroupDeleteResponse(true);
    }

    public async Task<Result<GetCostCenterWarehouseByIdResponse?>> GetCostCenterWarehouseById(
        GetCostCenterWarehouseByIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostCenterWarehouseById, Id:{Id}", request.CostCenterWarehouseId);

        var isValidRequest = await request.IsValidAsync<GetCostCenterWarehouseByIdValidator, GetCostCenterWarehouseByIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetCostCenterWarehouseByIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetCostCenterWarehouseByIdQuery(request.CostCenterWarehouseId), ct);
        if (response.IsFailure)
            return Result.Failure<GetCostCenterWarehouseByIdResponse>(response.Error!);

        var warehouseData = await WebServicesLogic.WarehouseDataReceiver(response.Value!.WarehouseId, _mediator, ct);
        var value = response.Value;
        return new GetCostCenterWarehouseByIdResponse(value.Id, value.WarehouseId, warehouseData?.WarehouseTypeId, warehouseData?.ManagerId,
            warehouseData?.Contact, warehouseData?.ManagerFullName, warehouseData?.Code, warehouseData?.Name, value.IsDefault);
    }

    public async Task<Result<GetsCostCenterWarehouseByCostCenterIdResponse?>> GetsCostCenterWarehouseByCostCenterId(
        GetsCostCenterWarehouseByCostCenterIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterWarehouseByCostCenterId, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterWarehouseByCostCenterIdValidator, GetsCostCenterWarehouseByCostCenterIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterWarehouseByCostCenterIdResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<GetsCostCenterWarehouseByCostCenterIdResponse>(costCenter.Error!);
        if (costCenter.Value is null)
            return Result.Failure<GetsCostCenterWarehouseByCostCenterIdResponse>(CostCenterErrors.CostCenterWithIdNotFound);

        var response = await _mediator.Send(new GetsCostCenterWarehouseByCostCenterIdQuery(request.CostCenterId, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsCostCenterWarehouseByCostCenterIdResponse>(response.Error!);
        if (response.Value?.Data?.Count <= 0)
            return Result.Failure<GetsCostCenterWarehouseByCostCenterIdResponse>(CostCenterWarehouseErrors.DataIsNull);

        var ids = response.Value?.Data?.Select(c => (long?)c.WarehouseId).Where(x => x != default).ToList();
        var warehousesData = await _mediator.Send(new GetsWarehouseByIdQuery(1, ids!.Count, ids), ct); // بره سراغ انبار

        var data = new List<CostCenterWarehouseModel>();
        foreach (var item in response.Value!.Data!)
        {
            var warehouse = warehousesData.Value?.Data?.Where(x => x.Id == item.WarehouseId).FirstOrDefault();
            data.Add(new CostCenterWarehouseModel(item.Id, item.WarehouseId, warehouse?.WarehouseTypeId, warehouse?.ManagerId,
                warehouse?.Contact, warehouse?.ManagerFullName, warehouse?.Code, warehouse?.Name, item.IsDefault));
        }

        return new GetsCostCenterWarehouseByCostCenterIdResponse(data ?? new List<CostCenterWarehouseModel>(0), response.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostCenterWarehouseByCostCenterIdsResponse?>> GetsCostCenterWarehouseByCostCenterIds(
        GetsCostCenterWarehouseByCostCenterIdsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterWarehouseByCostCenterIds, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterWarehouseByCostCenterIdsValidator, GetsCostCenterWarehouseByCostCenterIdsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterWarehouseByCostCenterIdsResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetsCostCenterWarehouseByCostCenterIdsQuery(request.CostCenterIds, request.PageIndex, request.PageSize), ct);
        if (response.IsFailure)
            return Result.Failure<GetsCostCenterWarehouseByCostCenterIdsResponse>(response.Error!);
        if (response.Value?.Data?.Count <= 0)
            return Result.Failure<GetsCostCenterWarehouseByCostCenterIdsResponse>(CostCenterWarehouseErrors.DataIsNull);
        var values = response.Value!.Data!;

        var ids = response.Value?.Data?.Select(c => (long?)c.WarehouseId).Where(x => x != default).ToList();
        var warehousesData = await _mediator.Send(new GetsWarehouseByIdQuery(1, ids!.Count, ids), ct); // بره سراغ انبار

        var datas = new List<GetsCostCenterWarehouseByCostCenterIdsModel>();
        foreach (var costCenterId in request.CostCenterIds)
        {
            var costCenterWarehouse = values.Where(x => x.CostCenter.Id == costCenterId).ToList();
            if (!costCenterWarehouse.Any())
                continue;

            var warehouses = new List<CostCenterWarehouseModel>();
            foreach (var item in costCenterWarehouse)
            {
                var warehouse = warehousesData.Value?.Data?.Where(x => x.Id == item.WarehouseId).FirstOrDefault();
                warehouses.Add(new CostCenterWarehouseModel(item.Id, item.WarehouseId, warehouse?.WarehouseTypeId, warehouse?.ManagerId,
                    warehouse?.Contact, warehouse?.ManagerFullName, warehouse?.Code, warehouse?.Name, item.IsDefault));
            }

            var costCenter = costCenterWarehouse.FirstOrDefault()!.CostCenter;
            datas.Add(new GetsCostCenterWarehouseByCostCenterIdsModel(costCenterId, costCenter.CostCenterCode, costCenter.CostCenterName, warehouses));
        }

        return new GetsCostCenterWarehouseByCostCenterIdsResponse(datas);
    }

    public async Task<Result<GetsCostCenterWarehouseInventoryResponse?>> GetsCostCenterWarehouseInventory(
        GetsCostCenterWarehouseInventoryRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterWarehouseInventory, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterWarehouseInventoryValidator, GetsCostCenterWarehouseInventoryRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterWarehouseInventoryResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<GetsCostCenterWarehouseInventoryResponse>(costCenter.Error!);
        if (costCenter.Value is null)
            return Result.Failure<GetsCostCenterWarehouseInventoryResponse>(CostCenterErrors.CostCenterWithIdNotFound);

        var responses = await _mediator.Send(new GetsCostCenterWarehouseInventoryQuery(request.CostCenterId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsCostCenterWarehouseInventoryResponse>(CostCenterWarehouseErrors.DataIsNull);
        var values = responses.Value.Data;
        var warehouseIds = values.Select(oo => oo.WarehouseId).Distinct().ToList();

        var getsWarehouse = await _mediator.Send(new GetProductInventoryByFilterQuery(request.ProductId, warehouseIds, request.FilterData, 0, 0), ct);
        if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null)
            return Result.Failure<GetsCostCenterWarehouseInventoryResponse>(RequestGoodsSupplyDetailErrors.InValidWarehouse);
        var warhouses = getsWarehouse.Value!.Data!;

        var data = new List<GetsCostCenterWarehouseInventoryResponseModel>();
        foreach (var item in warhouses)
        {
            var costCenterWarehouse = values.FirstOrDefault(x => x.WarehouseId == item.WareHouseId);
            var warhouse = new GetsCostCenterWarehouseInventoryResponseModel()
            {
                WarehouseId = item?.WareHouseId,
                WarehouseCode = item?.WareHouseCode,
                WarehouseName = item?.WareHouseName,
                InStockCount = item?.RealQuantity,
                RequestQuantity = item?.RequestQuantity,
                IsDefault = costCenterWarehouse is null ? false : costCenterWarehouse.IsDefault,
            };

            if (warhouse.IsDefault)
                warhouse.WarehouseName = $"{warhouse.WarehouseName}(پیش فرض)";

            data.Add(warhouse);
        }

        return new GetsCostCenterWarehouseInventoryResponse(data ?? new List<GetsCostCenterWarehouseInventoryResponseModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetsCostCenterWarehouseAssetsResponse?>> GetsCostCenterWarehouseAssets(
        GetsCostCenterWarehouseAssetsRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetsCostCenterWarehouseAssets, pageIndex:{PageIndex} , pageSize:{PageSize}", request.PageIndex, request.PageSize);

        var isValidRequest = await request.IsValidAsync<GetsCostCenterWarehouseAssetsValidator, GetsCostCenterWarehouseAssetsRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsCostCenterWarehouseAssetsResponse>(isValidRequest.Error!);

        var costCenter = await _mediator.Send(new GetCostCenterWithoutIncludeQuery(request.CostCenterId), ct);
        if (costCenter.IsFailure)
            return Result.Failure<GetsCostCenterWarehouseAssetsResponse>(costCenter.Error!);
        if (costCenter.Value is null)
            return Result.Failure<GetsCostCenterWarehouseAssetsResponse>(CostCenterErrors.CostCenterWithIdNotFound);

        var responses = await _mediator.Send(new GetsCostCenterWarehouseInventoryQuery(request.CostCenterId, request.PageIndex, request.PageSize), ct);
        if (responses.IsFailure || responses.Value is null || responses.Value.Data is null)
            return Result.Failure<GetsCostCenterWarehouseAssetsResponse>(CostCenterWarehouseErrors.DataIsNull);
        var values = responses.Value.Data;
        var warehouseIds = values.Select(oo => oo.WarehouseId).Distinct().ToList();

        long groupId = 0;
        List<FilteredInventory>? inventories = [];
        if (request.ProductId != null && request.ProductGroupId == null)
        {
            var productData = await WebServicesLogic.ProductDataReceiver(request.ProductId, _mediator, _pRepo, ct);
            if (productData is null)
                return Result.Failure<GetsCostCenterWarehouseAssetsResponse>(CostCenterWarehouseErrors.DataIsNull);
            groupId = productData.Group.Id;

            var getsProductWarehouse = await _mediator.Send(new GetProductInventoryByFilterQuery(request.ProductId.Value, warehouseIds, request.FilterData, 0, 0), ct);
            if (getsProductWarehouse.Value?.Data is not null)
                inventories = getsProductWarehouse.Value?.Data;
        }
        else
            groupId = request.ProductGroupId!.Value;

        var getsWarehouse = await _mediator.Send(new GetFilteredWarehousesByGroupIdQuery(warehouseIds, groupId, request.FilterData, 1, warehouseIds.Count), ct);
        if (getsWarehouse.IsFailure || getsWarehouse.Value is null || getsWarehouse.Value.Data is null)
            return Result.Failure<GetsCostCenterWarehouseAssetsResponse>(RequestGoodsSupplyDetailErrors.InValidWarehouse);
        var warhouses = getsWarehouse.Value!.Data!;

        var data = new List<GetsCostCenterWarehouseAssetsResponseModel>();
        foreach (var item in warhouses)
        {
            var costCenterWarehouse = values.FirstOrDefault(x => x.WarehouseId == item.Id);
            var inventory = inventories?.FirstOrDefault(x => x.WareHouseId == item.Id);
            var warhouse = new GetsCostCenterWarehouseAssetsResponseModel()
            {
                WarehouseId = item?.Id,
                WarehouseCode = item?.Code,
                WarehouseName = item?.Name,
                InStockCount = inventory == null ? 0 : inventory.RealQuantity,
                RequestQuantity = inventory == null ? 0 : inventory.RequestQuantity,
                IsDefault = costCenterWarehouse is null ? false : costCenterWarehouse.IsDefault,
            };

            if (warhouse.IsDefault)
                warhouse.WarehouseName = $"{warhouse.WarehouseName}(پیش فرض)";

            data.Add(warhouse);
        }

        return new GetsCostCenterWarehouseAssetsResponse(data ?? new List<GetsCostCenterWarehouseAssetsResponseModel>(0), responses.Value?.RowCount ?? 0);
    }

    public async Task<Result<GetDefaultCostCenterWarehouseByCostCenterIdResponse?>> GetDefaultCostCenterWarehouseByCostCenterId(
        GetDefaultCostCenterWarehouseByCostCenterIdRequest request, CT ct)
    {
        _logger.LogInformation("Request for GetCostCenterWarehouseById, Id:{Id}", request.CostCenterId);

        var isValidRequest = await request.IsValidAsync<GetDefaultCostCenterWarehouseByCostCenterIdValidator, GetDefaultCostCenterWarehouseByCostCenterIdRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetDefaultCostCenterWarehouseByCostCenterIdResponse>(isValidRequest.Error!);

        var response = await _mediator.Send(new GetDefaultByCostCenterIdQuery(request.CostCenterId), ct);
        if (response.IsFailure)
            return Result.Failure<GetDefaultCostCenterWarehouseByCostCenterIdResponse>(response.Error!);

        var warehouseData = await WebServicesLogic.WarehouseDataReceiver(response.Value!.WarehouseId, _mediator, ct);
        var value = response.Value;
        return new GetDefaultCostCenterWarehouseByCostCenterIdResponse(value.Id, value.WarehouseId, warehouseData?.WarehouseTypeId, warehouseData?.ManagerId,
                warehouseData?.Contact, warehouseData?.ManagerFullName, warehouseData?.Code, warehouseData?.Name, value.IsDefault);
    }
}