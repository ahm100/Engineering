using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

public record UpdateMachineryRequestModel(
    long? Id,
    long MachineryId,
    decimal? Number,
    decimal? UnusedPercentage,
    string FinalValue,
    RequestMachineryUnit? Unit,
    bool? IsDeleted
 );
