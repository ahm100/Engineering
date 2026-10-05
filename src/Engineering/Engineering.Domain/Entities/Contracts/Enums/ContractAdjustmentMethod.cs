namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractAdjustmentMethod
{
    [Description(ContractCmts.SingleBasisAdjustment)]
    SingleBasis = 1,

    [Description(ContractCmts.MultipleBasisAdjustment)]
    MultipleBasis = 2
}
