using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

public sealed record ContractChangeItemTerms(
    long? ContractTypeDetailId,
    long? ContractTypeId,
    long? ProjectOperationDetailId,
    PricingMethod PricingMethod,
    decimal PreviousValue,
    decimal NewValue,
    long? UnitOfMeasurementId,
    decimal? UnitPrice);
