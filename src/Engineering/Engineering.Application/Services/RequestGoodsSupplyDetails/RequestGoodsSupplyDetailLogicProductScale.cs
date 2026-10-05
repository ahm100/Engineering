using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProductByIdWithScale;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsGoodsSupplyDetailBySupplyProductIdWithScale;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails;

public partial class RequestGoodsSupplyDetailLogic : IRequestGoodsSupplyDetailLogic
{
    public async Task<Result<GetsGoodsSupplyProductByIdWithScaleResponse?>> GetsGoodsSupplyProductByIdWithScale(GetsGoodsSupplyProductByIdWithScaleRequest request, CT ct)
    {
        var isValidRequest = await request.IsValidAsync<GetsGoodsSupplyProductByIdWithScaleValidator, GetsGoodsSupplyProductByIdWithScaleRequest>(ct);
        if (isValidRequest.IsFailure)
            return Result.Failure<GetsGoodsSupplyProductByIdWithScaleResponse>(isValidRequest.Error!);

        List<long>? requestProductIds = await GetProductIdsForFilter(request.FilterProduct, request.ProductIds, ct);

        var responses = await _mediator.Send(new GetsGoodsSupplyDetailBySupplyProductIdWithScaleQuery(
            request.Ids,
            request.CostCenterIds,
            request.ProjectIds,
            request.ProjectOperationIds,
            request.ProjectOperationDetailIds,
            requestProductIds,
            request.CreatorIds,
            request.WarehouseIds,
            request.CityId,
            request.ProjectManagerId,
            request.Importances,
            request.Types,
            request.Statuses,
            request.RemoveStatuses,
            request.RequestNumber,
            request.FilterDescription,
            request.FilterPublicName,
            request.FilterOperationInfoName,
            request.FilterManagerDescription,
            request.FilterData,
            request.CustomerInvoiceNumber,
            request.FilterProduct,
            request.FromDate,
            request.ToDate,
            request.PageIndex,
            request.PageSize), ct);
        if (responses.IsFailure)
            return Result.Failure<GetsGoodsSupplyProductByIdWithScaleResponse>(responses.Error!);
        var values = responses.Value?.Data!;

        var measureUnitIds = values.Select(x => x.ProjectOperationMeasureId).Distinct().ToList();
        var measurments = await WebServicesLogic.MeasurementDataReceiver(measureUnitIds, _mediator, ct);

        var creatorIds = values.Where(x => x.CreatorId is not null && x.CreatorId > 0).Select(c => c.CreatorId!.Value).Distinct().ToList();
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var currencyIds = values.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(c => c.CurrencyId!.Value).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        var productIds = values.Where(x => x.ProductId is not null && x.ProductId > 0).Select(x => x.ProductId!.Value).Distinct().ToList();
        var products = await WebServicesLogic.ProductsDataReceiver(productIds, _mediator, _productRepo, ct);

        var packageIds = values.Where(x => x.PackageId is not null && x.PackageId > 0).Select(x => x.PackageId!.Value).Distinct().ToList();
        var packagesInfo = await WebServicesLogic.PackagesDataReceiver(packageIds, _mediator, ct);

        var metaIds = values.SelectMany(x => new long?[] { x.SupplyerId, x.BuyerId, x.ProjectManagerId, x.ContractorId })
             .Where(id => id.HasValue && id > 0).Select(id => id!.Value).Distinct().ToList();
        var metaInfos = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(metaIds, null, null, _mediator, ct);

        var warehouseIds = values.Where(x => x.DestinationWarehouseId is not null && x.DestinationWarehouseId > 0).Select(x => (long)x.DestinationWarehouseId!).Distinct().ToList();
        var warehouses = await WebServicesLogic.WarehousesDataReceiver(warehouseIds.Adapt<List<long?>>().ToList(), _mediator, ct);

        foreach (var item in values)
        {
            var creator = creators?.Where(m => m.UserId == item.CreatorId).FirstOrDefault();
            var measurment = measurments?.Where(m => m.Id == item.ProjectOperationMeasureId).FirstOrDefault();
            var currency = currencies?.Where(m => m.Id == item.CurrencyId).FirstOrDefault();
            var product = products?.Where(m => m.Id == item.ProductId).FirstOrDefault();
            var package = packagesInfo?.Where(x => x.Id.Equals(item.PackageId)).FirstOrDefault();
            var supplyer = metaInfos?.Where(x => x is not null && x.Id.Equals(item.SupplyerId)).FirstOrDefault();
            var buyer = metaInfos?.Where(x => x is not null && x.Id.Equals(item.BuyerId)).FirstOrDefault();
            var contractor = metaInfos?.Where(x => x is not null && x.Id.Equals(item.ContractorId)).FirstOrDefault();
            var warehouse = warehouses?.Where(x => x.Id.Equals(item.DestinationWarehouseId)).FirstOrDefault();
            var projectManager = metaInfos?.Where(x => x is not null && x.Id.Equals(item.ProjectManagerId)).FirstOrDefault();

            item.MeasurementName = measurment?.Name;
            item.Creator = creator?.FullName;
            item.CurrencyName = currency?.Name;
            item.ProductName = product?.Name;
            item.ProductCode = product?.Code;
            item.ProductBrand = product?.Brand;
            item.ProductBrandModel = product?.BrandModel;
            item.ProductGroupId = product?.Group.Id;
            item.ProductGroupName = product?.Group.Name;
            item.ProductGroupCode = product?.Group.Code;
            item.ProductGroupMeasurementName = product?.Group.Measure;
            item.PackageName = package?.Title;
            item.PackageQuantity = package?.Quantity;
            item.SupplyerFullName = supplyer?.FullName;
            item.BuyerFullName = buyer?.FullName;
            item.ContractorFullName = contractor?.FullName;
            item.DestinationWarehouseName = warehouse?.Name;
            item.ProjectManager = projectManager?.FullName;

            if (item.ScaleData != null && item.ScaleData.Count > 0)
            {
                var scales = item.ScaleData.Select(x => x.ReceiptNumber).ToList();

                var duplicates = scales.GroupBy(x => x)
                                   .Where(g => g.Count() > 1)
                                   .Select(g => g.Key)
                                   .ToList();

                item.HaveDuplicateBill = duplicates.Any() ? true : false;
            }
        }

        return new GetsGoodsSupplyProductByIdWithScaleResponse(values ?? new List<GetsGoodsSupplyDetailBySupplyProductIdWithScaleModel>(0), responses!.Value!.RowCount!);
    }
}