using Engineering.Domain.Entities.OperationInfos;
using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorServiceServiceInfo;

public record UpdateContractorServiceServiceInfoCommand(
    List<ProjectOperationDetailContractorService> ContractorServices,
    List<OperationInfoService> OperationInfoServices
    ) : ICommand<bool>;