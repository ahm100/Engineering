using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

public sealed record ContractSummaryChangeTerms(
    ContractChangeType Type,
    string Number,
    DateTime Date,
    string Subject,
    int? DurationChange,
    decimal PreviousContractAmount,
    decimal FinancialChangeAmount,
    decimal FinalContractAmount,
    IReadOnlyCollection<string> Urls);
