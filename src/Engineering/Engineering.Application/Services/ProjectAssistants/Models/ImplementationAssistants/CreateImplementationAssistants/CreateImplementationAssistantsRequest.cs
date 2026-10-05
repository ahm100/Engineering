namespace Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.CreateImplementationAssistants;

public record CreateImplementationAssistansRequest(
    long ProjectId,
    long ImplementationAssistantUserId
     ) : IHttpRequest;
