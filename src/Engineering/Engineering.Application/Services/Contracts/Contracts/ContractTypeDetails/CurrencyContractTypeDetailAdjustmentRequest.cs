using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractTypeDetails;

public record CurrencyContractTypeDetailAdjustmentRequest(
    DateTime BaseDate,
    decimal BaseRate,
    long CurrencyId,
    ContractAdjustmentCurrencyReferenceType ReferenceType,
    string? CustomReference);
