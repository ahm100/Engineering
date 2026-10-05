namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;

public record GetPOWbsByPOIdRequest(
    long ProjectOperationId,
    int PageIndex,
    int PageSize) : IHttpRequest;