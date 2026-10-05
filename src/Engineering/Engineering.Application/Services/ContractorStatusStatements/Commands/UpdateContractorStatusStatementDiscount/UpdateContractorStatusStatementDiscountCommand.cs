using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.UpdateContractorStatusStatementDiscount;

public record UpdateContractorStatusStatementDiscountCommand(
    long Id,
    decimal DiscountPrice,
    DateTime? RegistrationDate,
    string? Description
    ) : ICommand<ContractorStatusStatementDiscount>;
