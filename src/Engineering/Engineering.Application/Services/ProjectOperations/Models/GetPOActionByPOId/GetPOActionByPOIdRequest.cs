namespace Engineering.Application.Services.ProjectOperations.Models.GetPOActionByPOId;

public record GetPOActionByPOIdRequest(
    long ProjectOperationId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
