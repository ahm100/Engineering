namespace Engineering.Application.Services.ProjectWarehouses.Contracts.DeleteProjectWarehouses;

public record DeleteProjectWarehousesRequest(List<long> Ids) : IHttpRequest;
