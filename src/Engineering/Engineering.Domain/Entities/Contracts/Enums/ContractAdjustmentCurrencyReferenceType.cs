namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractAdjustmentCurrencyReferenceType
{
    [Description(ContractCmts.CentralBank)]
    CentralBank = 1,

    [Description(ContractCmts.ExchangeCenter)]
    ExchangeCenter = 2,

    [Description(ContractCmts.ContractualReference)]
    Contractual = 3,

    [Description(ContractCmts.OtherAdjustment)]
    Other = 4
}
