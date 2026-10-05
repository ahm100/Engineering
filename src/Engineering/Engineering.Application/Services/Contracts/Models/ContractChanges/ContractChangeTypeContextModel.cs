using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Models.ContractChanges;

public record ContractChangeTypeContextModel(
    long ContractTypeId,
    ContractTypeKind Kind,
    PricingMethod PricingMethod);
