namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractAdjustmentPeriod
{
    [Description(ContractCmts.FirstQuarter)]
    FirstQuarter = 1,

    [Description(ContractCmts.SecondQuarter)]
    SecondQuarter = 2,

    [Description(ContractCmts.ThirdQuarter)]
    ThirdQuarter = 3,

    [Description(ContractCmts.FourthQuarter)]
    FourthQuarter = 4
}
