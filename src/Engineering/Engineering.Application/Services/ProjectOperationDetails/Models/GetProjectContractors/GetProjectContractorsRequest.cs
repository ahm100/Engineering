namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectContractors;

public record GetProjectContractorsRequest(
    long ProjectId,
    int PageIndex,
    int PageSize) : IHttpRequest;