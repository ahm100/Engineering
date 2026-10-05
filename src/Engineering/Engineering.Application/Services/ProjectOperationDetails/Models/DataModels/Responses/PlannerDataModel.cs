namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;

public record PlannerDataModel(
    long? Id,
    long PlannerId,
    string? PlannerName,
    string? PlannerNickName
 );
