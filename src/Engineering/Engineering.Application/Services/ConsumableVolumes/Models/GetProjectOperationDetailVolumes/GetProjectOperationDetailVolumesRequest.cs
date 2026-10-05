namespace Engineering.Application.Services.ConsumableVolumes.Models.GetProjectOperationDetailVolumes;

public record GetProjectOperationDetailVolumesRequest(
    long ProjectOperationDetailId
     ) : IHttpRequest;
