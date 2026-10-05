namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehousesByProjectId;

public record GetProjectWarehousesByProjectIdRequest(
    long ProjectId,
    int PageIndex,
    int PageSize) : IHttpRequest;
