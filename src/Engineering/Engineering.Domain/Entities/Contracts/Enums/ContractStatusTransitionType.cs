namespace Engineering.Domain.Entities.Contracts.Enums;

public enum ContractStatusTransitionType
{
    [Description(ContractCmts.SubmitForReview)]
    SubmitForReview = 1,

    [Description(ContractCmts.ReturnToDraft)]
    ReturnToDraft = 2,

    [Description(ContractCmts.Approve)]
    Approve = 3,

    [Description(ContractCmts.Suspend)]
    Suspend = 4,

    [Description(ContractCmts.Resume)]
    Resume = 5,

    [Description(ContractCmts.Finish)]
    Finish = 6,

    [Description(ContractCmts.Terminate)]
    Terminate = 7
}
