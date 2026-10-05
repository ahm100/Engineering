using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.CreateContractorStatusStatementDiscount;

public record CreateContractorStatusStatementDiscountCommand(
    ContractorStatusStatement ContractorStatusStatement,
    RequestReward? RequestReward,
    decimal DiscountPrice,
    DateTime? RegistrationDate,
    string? Description
    ) : ICommand<ContractorStatusStatementDiscount>;
