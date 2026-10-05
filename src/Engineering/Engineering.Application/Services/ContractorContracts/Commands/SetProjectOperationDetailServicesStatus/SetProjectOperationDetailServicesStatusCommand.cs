using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ContractorContracts.Commands.SetProjectOperationDetailServicesStatus;

public record SetProjectOperationDetailServicesStatusCommand(
    long Id,
    ContractorServiceStatus Status
    ) : ICommand<ProjectOperationDetailContractorService>;