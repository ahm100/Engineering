namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractTypeDetailAdjustmentType
{
    [Description(ContractCmts.PriceIndexAdjustment)]
    PriceIndex = 1,

    [Description(ContractCmts.CurrencyAdjustment)]
    Currency = 2,

    [Description(ContractCmts.OtherAdjustment)]
    Other = 3
}
