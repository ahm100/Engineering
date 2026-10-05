namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectIds;

public record GetProjectWarehousesByProjectIdsRequest(
    List<long> ProjectIds,
    int PageIndex,
    int PageSize) : IHttpRequest;
