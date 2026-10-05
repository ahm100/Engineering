
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsConsumableVolumes;

public record GetsConsumableVolumesRequest(
    long ProjectOperationId,
    long? ProjectOperationDetailId,
    decimal FinalAmount
     ) : IHttpRequest;
