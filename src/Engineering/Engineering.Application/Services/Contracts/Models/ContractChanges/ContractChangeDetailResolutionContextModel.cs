using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Models.ContractChanges;

public record ContractChangeDetailResolutionContextModel(
    long ContractTypeDetailId,
    long ContractTypeId,
    ContractTypeKind Kind,
    PricingMethod PricingMethod,
    long SourceId,
    decimal BaselineQuantity,
    decimal? BaselineFixedAmount,
    long? UnitOfMeasurementId,
    decimal? UnitPrice,
    decimal? PriorNewValue,
    decimal? CurrentNewValue);
