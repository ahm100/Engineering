using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractStructure;

public record ContractStructureItem(
    long? Id,
    ContractTypeKind Kind,
    PricingMethod PricingMethod);
