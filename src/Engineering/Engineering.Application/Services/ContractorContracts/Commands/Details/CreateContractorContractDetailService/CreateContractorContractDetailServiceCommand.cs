using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailService;

public record CreateContractorContractDetailServiceCommand(
    ContractorContractDetail ContractorContractDetail,
    ProjectOperationDetailContractorService ContractorContractDetailService
    ) : ICommand<ContractorContractDetailService>;
