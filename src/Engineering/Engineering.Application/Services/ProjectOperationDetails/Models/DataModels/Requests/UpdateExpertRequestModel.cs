namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

public record UpdateExpertRequestModel(
    long? Id,
    string? TempId,
    long ExpertId,
    decimal? Number,
    decimal? UnusedPercentage,
    string FinalValue,
    bool? IsDeleted
 );
