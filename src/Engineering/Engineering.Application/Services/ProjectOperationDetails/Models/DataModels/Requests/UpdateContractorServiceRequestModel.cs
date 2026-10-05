using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

public record UpdateContractorServiceRequestModel(
    long? Id,
    string? TempId,
    long? ContractorId,
    long ServiceInfoId,
    long? ProjectServiceDetailId,
    decimal Volume,
    string TimeSpant,
    bool? IsDeleted,
    bool IsActive = true,
    PODContractorServiceType? Type = null
 );
