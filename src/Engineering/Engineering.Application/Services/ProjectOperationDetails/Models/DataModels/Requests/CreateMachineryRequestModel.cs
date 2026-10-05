using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

public record CreateMachineryRequestModel(
    long MachineryId,
    decimal? Number,
    decimal? UnusedPercentage,
    string FinalValue,
    RequestMachineryUnit? Unit,
    string? StandardValue
 );
