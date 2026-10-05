namespace Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.CreateTechnicalAssistans;

public record CreateTechnicalAssistansResponse(
    long Id,
    long TechnicalAssistantUserId,
    long ProjectId
    );
