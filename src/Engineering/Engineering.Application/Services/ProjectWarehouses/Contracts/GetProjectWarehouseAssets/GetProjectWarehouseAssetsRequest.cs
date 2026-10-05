namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseAssets;

public record GetProjectWarehouseAssetsRequest(
    long ProjectId,
    long? ProductGroupId,
    long? ProductId,
    string? FilterData,
    int PageIndex,
    int PageSize) : IHttpRequest;
