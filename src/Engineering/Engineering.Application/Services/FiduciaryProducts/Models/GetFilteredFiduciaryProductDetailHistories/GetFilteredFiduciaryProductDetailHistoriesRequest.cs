namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductDetailHistories;

public record GetFilteredFiduciaryProductDetailHistoriesRequest(long Id,
                                                                string[]? OrderBy,
                                                                int PageIndex,
                                                                int PageSize) : IHttpRequest;
