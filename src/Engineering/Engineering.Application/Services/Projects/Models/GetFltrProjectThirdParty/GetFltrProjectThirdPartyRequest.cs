namespace Engineering.Application.Services.Projects.Models.GetProjectThirdParties;

public record GetFltrProjectThirdPartyRequest(
    long ProjectId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
