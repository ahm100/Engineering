using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.Projects;
using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorService;

public record UpdateContractorServiceCommand(
    long Id,
    OperationInfoService OperationInfoService,
    ProjectServiceDetail? ProjectServiceDetail,
    long? ContractorId,
    decimal Volume,
    long TimeSpant,
    bool IsActive
    ) : ICommand<ProjectOperationDetailContractorService>;