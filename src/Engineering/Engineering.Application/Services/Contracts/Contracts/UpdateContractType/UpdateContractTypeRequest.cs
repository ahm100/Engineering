using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.UpdateContractType;

public record UpdateContractTypeRequest(
    long ContractId,
    long Id,
    ContractTypeKind Kind,
    PricingMethod PricingMethod) : IHttpRequest;
