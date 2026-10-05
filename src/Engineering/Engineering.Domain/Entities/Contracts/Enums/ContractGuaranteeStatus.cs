namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractGuaranteeStatus
{
    [Description(ContractCmts.GuaranteeActive)]
    Active = 1,

    [Description(ContractCmts.GuaranteeExtended)]
    Extended = 2,

    [Description(ContractCmts.GuaranteeReleased)]
    Released = 3,

    [Description(ContractCmts.GuaranteeExpired)]
    Expired = 4,

    [Description(ContractCmts.GuaranteeReturned)]
    Returned = 5
}
