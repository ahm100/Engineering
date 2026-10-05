namespace Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.ImplementationAssistantsModels;

public record ImplementationAssistansGetsByProjectIdModel(
    long Id,
    long ImplementationAssistantUserId,
    string ImplementationAssistantName
    );
