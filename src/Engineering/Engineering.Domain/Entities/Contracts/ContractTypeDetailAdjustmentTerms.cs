using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

public sealed record ContractTypeDetailAdjustmentTerms(
    ContractTypeDetailAdjustmentType Type,
    int? PriceIndexBaseYear,
    ContractAdjustmentPeriod? PriceIndexBasePeriod,
    long? PriceIndexId,
    DateTime? CurrencyBaseDate,
    decimal? CurrencyBaseRate,
    long? CurrencyId,
    ContractAdjustmentCurrencyReferenceType? CurrencyReferenceType,
    string? CurrencyCustomReference,
    string? OtherBasis,
    string? OtherReference,
    string? OtherIndex,
    string? Description);
