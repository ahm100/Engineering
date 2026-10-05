using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;
using Engineering.Domain.Errors.RequestGoodsSupplies;

namespace Engineering.Application.Services.GoodsManagerAssignments;

public partial class GoodsManagerAssignmentLogic
{
    private async Task<List<GetFilteredOrganizationModel>> EnrichAssignments(
    List<GetFilteredGoodsManagerAssignmentModel> entities,
    CT ct)
    {
        if (!entities.HasAny())
            return [];

        var organizationIds = entities.Listed(x => x.OrganizationId);
        var productIds = entities.Listed(x => x.ProductId);

        var organizations = organizationIds.HasAny()
            ? await _organizationRepo.GetByIds(organizationIds, ct)
            : [];

        var products = productIds.HasAny()
            ? await _productRepo.GetProductByIds(productIds, ct)
            : [];

        var organizationMap = organizations.ToDictionary(x => x.Id);
        var productMap = products.ToDictionary(x => x.Id);

        return entities
            .GroupBy(x => x.OrganizationId)
            .Select(group =>
            {
                var organization = organizationMap.GetValueOrDefault(group.Key);

                return new GetFilteredOrganizationModel
                {
                    OrganizationId = group.Key,
                    OrganizationFa = organization?.NameFa,
                    OrganizationEn = organization?.NameEn,

                    Products = group
                        .Select(x =>
                        {
                            var product = productMap.GetValueOrDefault(x.ProductId);

                            return new GetFilteredProductModel
                            {
                                ProductId = x.ProductId,
                                ProductName = product?.Name,
                                ProductCode = product?.Code
                            };
                        })
                        .DistinctBy(x => x.ProductId)
                        .ToList(),
                };
            })
            .ToList();
    }

    private async Task<Result<bool>> ValidateReferencesExist(
    long organizationId,
    long productId,
    CT ct)
    {
        var organization = await _organizationRepo.GetById(
            organizationId,
            ct);
        if (organization is null)
            return Result.Failure<bool>(
                GoodsManagerAssignmentErrors.OrganizationNotFound);

        var product = await _productRepo.GetProductById(
            productId,
            ct);
        if (product is null)
            return Result.Failure<bool>(
                GoodsManagerAssignmentErrors.ProductNotFound);

        return Result.Success(true);
    }

    private static List<GetFilteredOrganizationModel> ApplyFilter(
        List<GetFilteredOrganizationModel> models,
        string? filterData)
    {
        if (string.IsNullOrWhiteSpace(filterData))
            return models;

        var filter = filterData.Trim();

        return models
            .Where(x =>
                Contains(x.OrganizationFa, filter) ||
                Contains(x.OrganizationEn, filter) ||
                x.Products.Any(product =>
                    Contains(product.ProductName, filter))
            )
            .ToList();
    }

    private static bool Contains(
        string? value,
        string filter)
    {
        return !string.IsNullOrWhiteSpace(value) &&
               value.Contains(
                   filter,
                   StringComparison.OrdinalIgnoreCase);
    }
}