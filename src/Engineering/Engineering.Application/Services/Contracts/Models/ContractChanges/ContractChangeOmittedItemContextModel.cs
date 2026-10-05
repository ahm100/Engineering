using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Models.ContractChanges;

public record ContractChangeOmittedItemContextModel(
    long? ContractTypeDetailId,
    long? ContractTypeId,
    long SourceId,
    ContractTypeKind Kind,
    PricingMethod PricingMethod,
    decimal CurrentValue,
    decimal ProposedValue);
