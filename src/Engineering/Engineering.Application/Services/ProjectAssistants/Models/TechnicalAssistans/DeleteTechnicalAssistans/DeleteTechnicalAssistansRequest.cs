namespace Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.DeleteTechnicalAssistans;

public record DeleteTechnicalAssistansRequest(
    long TechnicalAssistansId,
    long ProjectId
     ) : IHttpRequest;
