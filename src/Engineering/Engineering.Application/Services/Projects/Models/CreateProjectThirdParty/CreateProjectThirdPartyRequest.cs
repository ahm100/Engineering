namespace Engineering.Application.Services.Projects.Models.AddAuthorizedThirdPartyToProject;

public record CreateProjectThirdPartyRequest(
    List<long> ThirdPartyIds,
    long ProjectId,
    long CreatorId
     ) : IHttpRequest;
