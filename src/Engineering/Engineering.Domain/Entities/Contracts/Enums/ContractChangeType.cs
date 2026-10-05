namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractChangeType
{
    [Description(ContractCmts.Addendum)]
    Addendum = 1,

    [Description(ContractCmts.Extension)]
    Extension = 2,

    [Description(ContractCmts.Correction)]
    Correction = 3,

    [Description(ContractCmts.OtherContractChange)]
    Other = 4
}
