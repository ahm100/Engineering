using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Abstractions.Data.WorkflowRequests;
using Engineering.Domain.Entities.CostOvers;
using Engineering.Domain.Entities.WorkflowRequests.Contracts;
using System.Globalization;

namespace Engineering.Application.Services.CostOvers.Commands.SubmitCostOverForApproval;

/// <summary>درخواست ایجادکننده برای آزادسازی موجودیت پس از دریافت نتیجه شکست یا لغو.</summary>
public sealed record RecoverCostOverApprovalCommand(
    long Id, long CompanyId, long UserId, Guid RequestId) : ICommand<CostOver>;

/// <summary>بستن تلاش قبلی و بازگشت هزینه به پیش نویس را در واحد کار مشترک انجام می دهد.</summary>
public sealed class RecoverCostOverApprovalCommandHandler :
    ICommandHandler<RecoverCostOverApprovalCommand, CostOver>
{
    private readonly ICostOverRepository _costOvers;
    private readonly IWorkflowIntegrationRepository _workflows;

    /// <summary>Repositoryهای موجودیت و درخواست در Context مشترک را دریافت می کند.</summary>
    public RecoverCostOverApprovalCommandHandler(
        ICostOverRepository costOvers, IWorkflowIntegrationRepository workflows)
    {
        _costOvers = costOvers;
        _workflows = workflows;
    }

    /// <summary>هویت، شرکت، تعلق تلاش و نتیجه نهایی را کنترل می کند؛ Commit در Logic انجام می شود.</summary>
    public async Task<Result<CostOver?>> Handle(RecoverCostOverApprovalCommand command, CT ct)
    {
        if (command.Id <= 0 || command.CompanyId <= 0 || command.UserId <= 0 ||
            command.RequestId == Guid.Empty)
            return Result.Failure<CostOver>(CostOverErrors.ApprovalRecoveryInvalid);

        var entity = await _costOvers.GetCostOverById(command.Id, ct);
        if (entity is null || entity.CompanyId != command.CompanyId)
            return Result.Failure<CostOver>(CostOverErrors.CostOverWithIdNotFound);
        if (entity.CreatorId != command.UserId)
            return Result.Failure<CostOver>(CostOverErrors.ApprovalRecoveryCreatorRequired);

        var request = await _workflows.Get(command.CompanyId, command.RequestId, ct);
        if (request is null || request.EntityType != CostOverWorkflowContract.EntityType ||
            request.Purpose != CostOverWorkflowContract.ApprovalPurpose ||
            request.BusinessKey != entity.Id.ToString(CultureInfo.InvariantCulture) ||
            request.RequestedByUserId != command.UserId)
            return Result.Failure<CostOver>(CostOverErrors.ApprovalRecoveryConflict);

        if (!request.IsOpen && request.FinishedAtUtc.HasValue &&
            request.Outcome is "Failed" or "Cancelled" &&
            entity.ApprovalStatus == CostOverApprovalStatus.Draft &&
            entity.CurrentApprovalAttemptId is null)
            return entity;

        if (!request.CanRecoverExecution() ||
            entity.ApprovalStatus != CostOverApprovalStatus.PendingApproval ||
            entity.CurrentApprovalAttemptId != request.RequestId)
            return Result.Failure<CostOver>(CostOverErrors.ApprovalRecoveryConflict);

        request.CloseInterrupted();
        entity.RecoverApproval(request.RequestId);
        await _costOvers.Update(entity);
        return entity;
    }
}