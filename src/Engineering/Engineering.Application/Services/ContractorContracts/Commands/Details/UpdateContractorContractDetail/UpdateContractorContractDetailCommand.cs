using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ContractorContracts.Commands.Details.UpdateContractorContractDetail;

public record UpdateContractorContractDetailCommand(
    ContractorContractDetail ContractorContractDetail,
    ProjectOperation? ProjectOperation,
    DateTime? StartDate,
    DateTime? EndDate,
    decimal? WorkLoad,
    decimal? UnitAmount,
    decimal ContractCoefficient
    ) : ICommand<ContractorContractDetail>;
