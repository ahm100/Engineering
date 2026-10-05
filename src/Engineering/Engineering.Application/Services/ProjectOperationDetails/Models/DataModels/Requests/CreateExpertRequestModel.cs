namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

public record CreateExpertRequestModel(
    string? TempId,
    long ExpertId,
    decimal? Number,
    decimal? UnusedPercentage,
    string FinalValue,
    string? StandardValue
 );
