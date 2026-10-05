using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

public record CreateContractorServiceRequestModel(
    string? TempId,
    long? ContractorId,
    long ServiceInfoId,
    long? ProjectServiceDetailId,
    decimal Volume,
    string TimeSpant,
    bool IsActive,
    PODContractorServiceType? Type = null
 );
