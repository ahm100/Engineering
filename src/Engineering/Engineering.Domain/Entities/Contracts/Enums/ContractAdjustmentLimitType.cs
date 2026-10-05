namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractAdjustmentLimitType
{
    [Description(ContractCmts.AdjustmentLimitAmount)]
    Amount = 1,

    [Description(ContractCmts.AdjustmentLimitPercentage)]
    Percentage = 2
}