using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Commands.ContractorStatusStatementStatusChanger;

public record ContractorStatusStatementStatusChangerCommand(
    ContractorStatusStatement Entity,
    CSSStatus Status,
    decimal? ConfirmedAmount,
    string? Description,
    bool? MultiPayment,
    string? LastDescription,
    long? PaymentOrderId,
    List<string>? Urls
    ) : ICommand<ContractorStatusStatement>;
