namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractGuaranteeType
{
    [Description(ContractCmts.PerformanceGuarantee)]
    PerformanceGuarantee = 1,

    [Description(ContractCmts.PrepaymentGuarantee)]
    Prepayment = 2,

    [Description(ContractCmts.WarrantyPeriodGuarantee)]
    WarrantyPeriod = 3,

    [Description(ContractCmts.OtherGuarantee)]
    Other = 4
}
