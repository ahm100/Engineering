namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractStatus
{
    [Description(ContractCmts.Draft)]
    Draft = 1,

    [Description(ContractCmts.UnderReview)]
    UnderReview = 2,

    [Description(ContractCmts.Active)]
    Active = 3,

    [Description(ContractCmts.Suspended)]
    Suspended = 4,

    [Description(ContractCmts.Finished)]
    Finished = 5,

    [Description(ContractCmts.Terminated)]
    Terminated = 6
}
