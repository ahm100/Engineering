namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationId;

public record GetMachineriesByProjectOperationIdRequest(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
