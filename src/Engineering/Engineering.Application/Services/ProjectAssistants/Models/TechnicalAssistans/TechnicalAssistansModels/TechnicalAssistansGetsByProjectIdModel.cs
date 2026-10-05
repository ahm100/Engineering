namespace Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.TechnicalAssistansModels;

public record TechnicalAssistansGetsByProjectIdModel(
    long Id,
    long TechnicalAssistantUserId,
    string ImplementationAssistantName
    );
