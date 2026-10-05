using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.CreateContractType;

public record CreateContractTypeRequest(
    long ContractId,
    ContractTypeKind Kind,
    PricingMethod PricingMethod) : IHttpRequest;
