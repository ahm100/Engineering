namespace Engineering.Application.Services.ProjectWarehouses.Contracts.SaveProjectWarehouses;

public record SaveProjectWarehousesRequest(
    long ProjectId,
    List<SaveProjectWarehouseModel> ProjectWarehouses) : IHttpRequest;
