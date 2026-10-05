using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractAdjustmentConfigurations;

public record ContractAdjustmentScopeRequest(
    bool WholeContract,
    List<ContractTypeKind>? ContractTypeKinds,
    List<long>? ContractTypeDetailIds);
