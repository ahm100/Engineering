using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Services;

public record ContractorServiceModel(
    string? TempId,
    long? ContractorId,
    string? ContractorName,
    OperationInfoService OperationInfoService,
    ProjectServiceDetail? ProjectServiceDetail,
    decimal Volume,
    long TimeSpant,
    bool IsActive,
    PODContractorServiceType Type = PODContractorServiceType.ServiceBased
 );
