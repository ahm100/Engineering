namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractAdjustmentScopeType
{
    [Description(ContractCmts.WholeContract)]
    WholeContract = 1,

    [Description(ContractCmts.ContractTypeKindScope)]
    ContractTypeKind = 2,

    [Description(ContractCmts.ContractTypeDetailScope)]
    ContractTypeDetail = 3
}
