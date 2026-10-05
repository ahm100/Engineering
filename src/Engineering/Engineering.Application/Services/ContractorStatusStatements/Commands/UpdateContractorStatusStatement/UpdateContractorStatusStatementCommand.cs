using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.UpdateContractorStatusStatement;

public record UpdateContractorStatusStatementCommand(
    ContractorStatusStatement Entity,
    decimal? UpdatorConfirmedAmount,
    string? Description,
    List<string>? Urls
    ) : ICommand<ContractorStatusStatement>;
