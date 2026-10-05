namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;

public record TechnicalAssistantDataModel(
    long? Id,
    long TechnicalAssistantId,
    string? TechnicalAssistantName,
    string? TechnicalAssistantNickName
 );
