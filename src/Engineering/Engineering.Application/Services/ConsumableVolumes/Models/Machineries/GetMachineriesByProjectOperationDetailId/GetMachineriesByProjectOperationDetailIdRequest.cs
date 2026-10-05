namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetMachineriesByProjectOperationDetailId;

public record GetMachineriesByProjectOperationDetailIdRequest(
    long ProjectOperationDetailId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
