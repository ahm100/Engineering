using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Commands.ContractorContracts.DeleteContractorContract;

public record DeleteContractorContractCommand(
    long Id,
    long CompanyId
    ) : ICommand<ContractorContract>;
