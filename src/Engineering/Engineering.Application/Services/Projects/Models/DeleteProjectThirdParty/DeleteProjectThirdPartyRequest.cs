namespace Engineering.Application.Services.Projects.Models.DeleteProjectThirdParty;

public record DeleteProjectThirdPartyRequest(
    List<long> Ids
     ) : IHttpRequest;