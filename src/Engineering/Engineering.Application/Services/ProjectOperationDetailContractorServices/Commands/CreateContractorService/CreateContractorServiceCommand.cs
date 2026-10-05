using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.Projects;
using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.CreateContractorService;

public record CreateContractorServiceCommand(
    ProjectOperationDetail ProjectOperationDetail,
    OperationInfoService OperationInfoService,
    ProjectServiceDetail? ProjectServiceDetail,
    long? ContractorId,
    decimal Volume,
    long TimeSpant,
    bool IsActive,
    PODContractorServiceType Type = PODContractorServiceType.ServiceBased
    ) : ICommand<ProjectOperationDetailContractorService>;