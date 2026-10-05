using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFilteredFiduciaryProductDetailHistories;

public record GetFilteredFiduciaryProductDetailHistoriesQuery(long FiduciaryProductId,
                                                              string[]? OrderBy,
                                                              int PageIndex,
                                                              int PageSize) : IQuery<DataResult<List<GetFilteredFiduciaryProductDetailHistoriesModel>>>;

