using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractSummaryChange;

public record UpdateContractSummaryChangeRequest(
    long ContractId,
    long Id,
    ContractChangeType Type,
    string Number,
    DateTime Date,
    string Subject,
    decimal FinancialChangeAmount,
    decimal? NewContractAmount,
    int? DurationChange,
    DateTime? NewEndDate,
    List<string>? Urls) : IHttpRequest;
