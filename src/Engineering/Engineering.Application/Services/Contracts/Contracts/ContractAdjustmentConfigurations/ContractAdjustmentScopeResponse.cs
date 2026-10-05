using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Contracts.Contracts.ContractAdjustmentConfigurations;

public class ContractAdjustmentScopeResponse
{
    public bool WholeContract { get; set; }
    public List<ContractTypeKind> ContractTypeKinds { get; set; } = [];
    public List<long> ContractTypeDetailIds { get; set; } = [];
}
