namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationDetailId;

public record GetProductsByProjectOperationDetailIdRequest(
    long ProjectOperationDetailId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
