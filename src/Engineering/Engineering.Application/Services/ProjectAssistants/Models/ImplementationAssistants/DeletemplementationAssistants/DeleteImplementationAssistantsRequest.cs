namespace Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.DeletemplementationAssistants;

public record DeleteImplementationAssistansRequest(
    long ImplementationAssistantUserId,
    long ProjectId
     ) : IHttpRequest;
