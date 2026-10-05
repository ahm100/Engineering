namespace Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.CreateTechnicalAssistans;

public record CreateTechnicalAssistansRequest(
    long TechnicalAssistantUserId,
    long ProjectId
     ) : IHttpRequest;
