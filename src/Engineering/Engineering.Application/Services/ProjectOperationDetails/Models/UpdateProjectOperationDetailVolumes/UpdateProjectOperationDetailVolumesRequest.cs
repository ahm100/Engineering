namespace Engineering.Application.Services.ProjectOperationDetails.Models.UpdateProjectOperationDetailVolumes;

public record UpdateProjectOperationDetailVolumesRequest(
    long? projectOperationId,
    List<long>? ProjectOperationDetailIds) : IHttpRequest;

