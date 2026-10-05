namespace Engineering.Domain.Entities.Contracts.Enums;

public enum PrepaymentAmortizationMethod
{
    [Description(ContractCmts.FixedPercentagePerStatusStatement)]
    FixedPercentagePerStatusStatement = 1,

    [Description(ContractCmts.FixedAmountPerStatusStatement)]
    FixedAmountPerStatusStatement = 2,

    [Description(ContractCmts.AfterSpecificProgressPercentage)]
    AfterSpecificProgressPercentage = 3
}