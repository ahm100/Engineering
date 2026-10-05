using Engineering.Application.Services.ProjectWarehouses.Contracts;
using Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseAssets;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredWarehousesByGroupId;
using Engineering.Application.WebServices.WarehouseServices.Inventories.Models.GetProductInventoryByFilter;
using Engineering.Application.WebServices.WarehouseServices.Inventories.Queries.GetProductInventoryByFilter;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetsById;

namespace Engineering.Application.Services.ProjectWarehouses;

public partial class ProjectWarehouseLogic
{
    private async Task<bool> EnrichWarehouses(List<ProjectWarehouseModel> models, CT ct)
    {
        var warehouseIds = models.Listed(oo => oo.WarehouseId);
        var result = await _mediator.Send(
            new GetsWarehouseByIdQuery(1, warehouseIds.Count, warehouseIds.Select(oo => (long?)oo).ToList()),
            ct);

        if (result.IsBad() || result.Value?.Data is null)
            return false;

        var warehouses = result.Value.Data.ToDictionary(oo => oo.Id);
        foreach (var model in models)
        {
            if (!warehouses.TryGetValue(model.WarehouseId, out var warehouse))
                continue;

            model.WarehouseTypeId = warehouse.WarehouseTypeId;
            model.ManagerId = warehouse.ManagerId;
            model.ManagerContact = warehouse.Contact;
            model.ManagerFullName = warehouse.ManagerFullName;
            model.Code = warehouse.Code;
            model.Name = warehouse.Name;
        }

        return models.All(oo => warehouses.ContainsKey(oo.WarehouseId));
    }

    private async Task<Result<GetProjectWarehouseAssetsResponse?>> GetProjectWarehouseAssets(
        GetProjectWarehouseAssetsRequest request,
        List<ProjectWarehouseModel> associations,
        int rowCount,
        CT ct)
    {
        var warehouseIds = associations.Listed(oo => oo.WarehouseId);
        var groupId = request.ProductGroupId ?? 0;
        List<FilteredInventory>? inventories = [];

        if (request.ProductId is not null && request.ProductGroupId is null)
        {
            var product = await WebServicesLogic.ProductDataReceiver(
                request.ProductId,
                _mediator,
                _productRepository,
                ct);
            if (product is null)
                return Result.Failure<GetProjectWarehouseAssetsResponse>(ProjectWarehouseErrors.WarehouseNotFound);

            groupId = product.Group.Id;
            var inventory = await _mediator.Send(
                new GetProductInventoryByFilterQuery(request.ProductId.Value, warehouseIds, request.FilterData, 0, 0),
                ct);
            if (!inventory.IsBad() && inventory.Value?.Data is not null)
                inventories = inventory.Value.Data;
        }

        var result = await _mediator.Send(
            new GetFilteredWarehousesByGroupIdQuery(
                warehouseIds,
                groupId,
                request.FilterData,
                1,
                warehouseIds.Count),
            ct);
        if (result.IsBad() || result.Value?.Data is null)
            return Result.Failure<GetProjectWarehouseAssetsResponse>(ProjectWarehouseErrors.WarehouseNotFound);

        var data = result.Value.Data.Select(oo =>
        {
            var association = associations.FirstOrDefault(xx => xx.WarehouseId == oo.Id);
            var inventory = inventories?.FirstOrDefault(xx => xx.WareHouseId == oo.Id);
            return new GetProjectWarehouseAssetsModel
            {
                WarehouseId = oo.Id,
                WarehouseCode = oo.Code,
                WarehouseName = association?.IsDefault == true ? $"{oo.Name}(پیش فرض)" : oo.Name,
                InStockCount = inventory?.RealQuantity ?? 0,
                RequestQuantity = inventory?.RequestQuantity ?? 0,
                IsDefault = association?.IsDefault == true
            };
        }).ToList();

        return new GetProjectWarehouseAssetsResponse(data, rowCount);
    }
}
