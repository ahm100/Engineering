using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Queries.GetFilteredFiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts;

public partial class FiduciaryProductLogic : IFiduciaryProductLogic
{
    private List<long>? GetUserIds(
        GetFiduciaryProductByIdResponse? fiduciaryProduct)
    {
        if (fiduciaryProduct is null)
            return null;

        List<long>? userIds = [fiduciaryProduct.CreatorId != null && fiduciaryProduct.CreatorId > 0 ? fiduciaryProduct.CreatorId.Value : 0];
        userIds.AddRange(fiduciaryProduct.Details?.Where(x => x.CreatorId is not null && x.CreatorId > 0).Select(x => x.CreatorId!.Value).Distinct().ToList() ?? []);
        userIds.AddRange(fiduciaryProduct.Details?
            .Where(x => x.Managements != null && x.Managements.Count > 0)
            .SelectMany(x => x.Managements!).Where(z => z.CreatorId is not null && z.CreatorId > 0)
            .Select(z => z.CreatorId!.Value).Distinct().ToList() ?? []);
        userIds.AddRange(fiduciaryProduct.Details?
            .Where(x => x.Managements != null && x.Managements.Count > 0)
            .SelectMany(x => x.Managements!)
            .Where(r => r.Returns != null && r.Returns.Count > 0)
            .SelectMany(r => r.Returns!)
            .Where(z => z.CreatorId is not null && z.CreatorId > 0)
            .Select(z => z.CreatorId!.Value).Distinct().ToList() ?? []);

        return userIds.Distinct().ToList();
    }

    private List<long>? GetUserIds(
        List<GetFilteredFiduciaryProductsModel>? fiduciaryProducts)
    {
        if (fiduciaryProducts is null || fiduciaryProducts.Count <= 0)
            return null;

        List<long>? creatorIds = [];
        creatorIds.AddRange(fiduciaryProducts?.Where(c => c.CreatorId is not null && c.CreatorId > 0)
            .Select(c => c.CreatorId!.Value)
            .Distinct()
            .ToList() ?? []);
        creatorIds.AddRange(fiduciaryProducts?.Where(c => c.Details is not null && c.Details.Count > 0)
            .SelectMany(c => c.Details!)
            .Where(c => c.CreatorId is not null && c.CreatorId > 0)
            .Select(c => c.CreatorId!.Value)
            .Distinct()
            .ToList() ?? []);

        return creatorIds.Distinct().ToList();
    }

    private List<long>? GetCurrencyIds(
        GetFiduciaryProductByIdResponse? fiduciaryProduct)
    {
        if (fiduciaryProduct is null)
            return null;

        List<long>? currencyIds = [];
        currencyIds.AddRange(fiduciaryProduct.Details?.Where(x => x.CurrencyId is not null && x.CurrencyId > 0).Select(x => x.CurrencyId!.Value).Distinct().ToList() ?? []);
        currencyIds.AddRange(fiduciaryProduct.Details?
            .Where(x => x.Managements != null && x.Managements.Count > 0)
            .SelectMany(x => x.Managements!)
            .Where(r => r.Returns != null && r.Returns.Count > 0)
            .SelectMany(r => r.Returns!)
            .Where(z => z.CurrencyId is not null && z.CurrencyId > 0)
            .Select(z => z.CurrencyId!.Value).Distinct().ToList() ?? []);

        return currencyIds.Distinct().ToList();
    }

    private async Task<Result<(List<GetFilteredFiduciaryProductsModel> Data, int RowCount)>> GetFilteredFiduciaryProductsData(
        List<long>? ids,
        long? costCenterId,
        long? projectId,
        FiduciaryProductStatus? status,
        List<long>? projectOperationIds,
        long? thirdPartyId,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        IMediator _mediator,
        CT ct)
    {
        var response = await _mediator.Send(new GetFilteredFiduciaryProductsQuery(
            ids,
            costCenterId,
            projectId,
            status,
            projectOperationIds,
            thirdPartyId,
            fromDate,
            toDate,
            filterData,
            orderBy,
            pageIndex,
            pageSize),
            ct);
        if (response.IsFailure || response.Value is null || response.Value.Data is null)
            return Result.Failure<(List<GetFilteredFiduciaryProductsModel> Data, int RowCount)>(response.Error!);
        var fiduciaryProducts = response.Value!.Data!;

        var companyIds = fiduciaryProducts!.Where(x => x.CompanyId != null && x.CompanyId > 0)
            .Select(x => (long)x.CompanyId!).ToList();
        var companies = await WebServicesLogic.CompaniesDataReceiver(companyIds, _mediator, ct);

        var thirdPartyIds = fiduciaryProducts!.Where(x => x.ThirdPartyId is not null && x.ThirdPartyId > 0)
            .Select(c => c.ThirdPartyId!.Value).Distinct().ToList();
        var thirdParties = await WebServicesLogic.GetWithSkillOnlyByIdsReceiver(thirdPartyIds, null, null, _mediator, ct);

        var creatorIds = GetUserIds(fiduciaryProducts);
        var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

        var productIds = fiduciaryProducts!.Where(x => x.Details is not null && x.Details.Count > 0)
            .SelectMany(x => x.Details!).Where(x => x.ProductId is not null && x.ProductId > 0)
            .Select(c => (long)c.ProductId!).Distinct().ToList();
        var products = await WebServicesLogic.ProductsDataReceiver(productIds, _mediator, _pRepo, ct);

        var measureIds = fiduciaryProducts!.Where(x => x.Details is not null && x.Details.Count > 0)
            .SelectMany(x => x.Details!).Where(x => x.MeasureunitId is not null && x.MeasureunitId > 0)
            .Select(c => (long)c.MeasureunitId!).Distinct().ToList();
        var measures = await WebServicesLogic.MeasurementDataReceiver(measureIds, _mediator, ct);

        var currencyIds = fiduciaryProducts!.Where(x => x.Details is not null && x.Details.Count > 0)
            .SelectMany(x => x.Details!).Where(x => x.CurrencyId is not null && x.CurrencyId > 0)
            .Select(c => (long)c.CurrencyId!).Distinct().ToList();
        var currencies = await WebServicesLogic.CurrenciesDataReceiver(currencyIds, _mediator, ct);

        fiduciaryProducts!.ForEach(item =>
        {
            item.Creator = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            item.CompanyNameFa = companies?.FirstOrDefault(x => x.Id == item.CompanyId)?.NameFa;
            item.ThirdParty = thirdParties?.FirstOrDefault(x => x is not null && x.Id == item.ThirdPartyId)?.FullName;
            if (item.Details is not null && item.Details.Count > 0)
                item.Details.ForEach(item1 =>
                {
                    item1.Creator = creators?.FirstOrDefault(x => x.UserId == item1.CreatorId)?.FullName;
                    item1.ProductCode = products?.FirstOrDefault(c => c.Id == item1.ProductId)?.Code;
                    item1.ProductName = products?.FirstOrDefault(c => c.Id == item1.ProductId)?.Name;
                    item1.ProductBrand = products?.FirstOrDefault(c => c.Id == item1.ProductId)?.Brand;
                    item1.ProductBrandModel = products?.FirstOrDefault(c => c.Id == item1.ProductId)?.BrandModel;
                    item1.MeasureunitName = measures?.FirstOrDefault(c => c.Id == item1.MeasureunitId)?.Name;
                    item1.CurrencyName = currencies?.FirstOrDefault(c => c.Id == item1.CurrencyId)?.Name;
                });
        });

        return (fiduciaryProducts, response.Value?.RowCount ?? 0);
    }
}

