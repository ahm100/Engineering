using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFilteredFiduciaryProducts;

public record GetFilteredFiduciaryProductsQuery(
    List<long>? Ids,
    long? CostCenterId,
    long? ProjectId,
    FiduciaryProductStatus? Status,
    List<long>? ProjectOperationIds,
    long? ThirdPartyId,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<GetFilteredFiduciaryProductsModel>>>;
