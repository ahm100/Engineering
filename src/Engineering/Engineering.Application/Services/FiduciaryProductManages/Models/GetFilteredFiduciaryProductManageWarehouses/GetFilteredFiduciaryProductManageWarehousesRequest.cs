namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFilteredFiduciaryProductManageWarehouses;

public record GetFilteredFiduciaryProductManageWarehousesRequest(long FiduciaryProductDetailId,
                                                                 int PageIndex,
                                                                 int PageSize) : IHttpRequest;
