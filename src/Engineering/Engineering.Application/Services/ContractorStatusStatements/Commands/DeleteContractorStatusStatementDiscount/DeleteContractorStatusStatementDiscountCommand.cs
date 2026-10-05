using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.DeleteContractorStatusStatementDiscount;

public record DeleteContractorStatusStatementDiscountCommand(
    long Id
    ) : ICommand<ContractorStatusStatementDiscount>;
