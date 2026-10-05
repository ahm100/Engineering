using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.CreateContractorContractDetail;

public record CreateContractorContractDetailCommand(
    ContractorContract ContractorContract,
    ProjectOperation? ProjectOperation,
    DateTime? StartDate,
    DateTime? EndDate,
    decimal? WorkLoad,
    decimal? UnitAmount,
    decimal ContractCoefficient
    ) : ICommand<ContractorContractDetail>;
