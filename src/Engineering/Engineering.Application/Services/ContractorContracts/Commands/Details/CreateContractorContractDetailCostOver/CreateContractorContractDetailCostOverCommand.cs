using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetailCostOver;

public record CreateContractorContractDetailCostOverCommand(
    ContractorContract? ContractorContract,
    ContractorContractDetail? ContractorContractDetail,
    CostOver CostOver,
    long ContractorId,
    decimal Percentage,
    string? Description
    ) : ICommand<ContractorContractDetailCostOver>;
