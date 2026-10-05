using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;

namespace Engineering.Application.Services.FiduciaryProducts.Queries.GetFilteredFiduciaryProductHistories;

public record GetFilteredFiduciaryProductHistoriesQuery(long FiduciaryProductId,
                                                        string[]? OrderBy,
                                                        int PageIndex,
                                                        int PageSize) : IQuery<DataResult<List<GetFilteredFiduciaryProductHistoriesModel>>>;

