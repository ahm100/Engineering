namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductsByProjectOperationId;

public record GetProductsByProjectOperationIdRequest(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
