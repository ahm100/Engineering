namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductHistories;

public record GetFilteredFiduciaryProductHistoriesRequest(long Id,
                                                          string[]? OrderBy,
                                                          int PageIndex,
                                                          int PageSize) : IHttpRequest;

