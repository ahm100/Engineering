using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Commands.UpdateContractorServiceContractor;

public record UpdateContractorServiceContractorCommand(
    long Id,
    long ContractorId
    ) : ICommand<ProjectOperationDetailContractorService>;