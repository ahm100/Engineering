using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.CreateContractSummaryChange;

public record CreateContractSummaryChangeRequest(
    long ContractId,
    ContractChangeType Type,
    string Number,
    DateTime Date,
    string Subject,
    decimal FinancialChangeAmount,
    decimal? NewContractAmount,
    int? DurationChange,
    DateTime? NewEndDate,
    List<string>? Urls) : IHttpRequest;
