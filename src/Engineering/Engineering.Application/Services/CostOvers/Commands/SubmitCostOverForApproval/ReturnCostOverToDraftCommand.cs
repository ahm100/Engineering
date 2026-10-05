using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Abstractions.Data.WorkflowRequests;
using Engineering.Domain.Entities.CostOvers;

namespace Engineering.Application.Services.CostOvers.Commands.SubmitCostOverForApproval;

/// <summary>بازگشت هزینه ردشده به پیش نویس در شرکت جاری.</summary>
public sealed record ReturnCostOverToDraftCommand(long Id, long CompanyId) : ICommand<CostOver>;

/// <summary>بستن تلاش ردشده و بازگشت موجودیت را بدون Commit انجام می دهد.</summary>
public sealed class ReturnCostOverToDraftCommandHandler : ICommandHandler<ReturnCostOverToDraftCommand, CostOver>
{
    private readonly ICostOverRepository _costOvers;
    private readonly IWorkflowIntegrationRepository _workflows;
    /// <summary>Repositoryهای موجودیت و درخواست گردش کار در واحد کار جاری را دریافت می کند.</summary>
    public ReturnCostOverToDraftCommandHandler(
        ICostOverRepository costOvers, IWorkflowIntegrationRepository workflows)
    {
        _costOvers = costOvers;
        _workflows = workflows;
    }

    /// <summary>تنها تلاش ردشده جاری را می بندد تا ارسال بعدی شناسه جدید داشته باشد.</summary>
    public async Task<Result<CostOver?>> Handle(ReturnCostOverToDraftCommand command, CT ct)
    {
        var entity = await _costOvers.GetCostOverById(command.Id, ct);
        if (entity is null || entity.CompanyId != command.CompanyId)
            return Result.Failure<CostOver>(CostOverErrors.CostOverWithIdNotFound);

        if (entity.ApprovalStatus != CostOverApprovalStatus.Rejected || !entity.CurrentApprovalAttemptId.HasValue)
            return Result.Failure<CostOver>(CostOverErrors.ApprovalConflict);

        var request = await _workflows.Get(command.CompanyId, entity.CurrentApprovalAttemptId.Value, ct);
        if (request is null || request.Outcome != "Rejected" || !request.IsOpen)
            return Result.Failure<CostOver>(CostOverErrors.ApprovalConflict);

        request.CloseRejected();
        entity.ReturnToDraft();
        await _costOvers.Update(entity);
        return entity;
    }
}
