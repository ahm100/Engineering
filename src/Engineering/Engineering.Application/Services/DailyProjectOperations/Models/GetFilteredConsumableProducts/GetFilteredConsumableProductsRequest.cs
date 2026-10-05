
namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredConsumableProducts;

public record GetFilteredConsumableProductsRequest(long ProjectOperationDetailId,
                                                   string? FilterData,
                                                   int PageIndex,
                                                   int PageSize) : IHttpRequest;
