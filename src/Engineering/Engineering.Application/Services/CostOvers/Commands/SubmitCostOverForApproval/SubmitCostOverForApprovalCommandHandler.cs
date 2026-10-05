using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Abstractions.Data.WorkflowRequests;
using Engineering.Application.Services.WorkflowRequests;
using Engineering.Domain.Entities.CostOvers;
using Engineering.Domain.Entities.WorkflowRequests;
using Engineering.Domain.Entities.WorkflowRequests.Contracts;
using System.Globalization;

namespace Engineering.Application.Services.CostOvers.Commands.SubmitCostOverForApproval;

/// <summary>تغییر وضعیت و ثبت درخواست و Outbox را در واحد کار جاری انجام می دهد.</summary>
public sealed class SubmitCostOverForApprovalCommandHandler : ICommandHandler<SubmitCostOverForApprovalCommand, WorkflowRequest>
{
    private readonly ICostOverRepository _costOvers;
    private readonly IWorkflowIntegrationRepository _workflows;

    /// <summary>Repositoryهای دارای Context مشترک را دریافت می کند.</summary>
    public SubmitCostOverForApprovalCommandHandler(ICostOverRepository costOvers, IWorkflowIntegrationRepository workflows)
    {
        _costOvers = costOvers;
        _workflows = workflows;
    }

    /// <summary>شرکت و ایجادکننده را کنترل و تغییر وضعیت، درخواست و پیام را بدون Commit ثبت می کند؛ ارسال تکراری تلاش جاری را برمی گرداند.</summary>
    public async Task<Result<WorkflowRequest?>> Handle(SubmitCostOverForApprovalCommand command, CT ct)
    {
        if (command.Id <= 0 || command.CompanyId <= 0 || command.UserId <= 0)
            return Result.Failure<WorkflowRequest>(GlobalErrors.InvalidCompany);

        var entity = await _costOvers.GetCostOverById(command.Id, ct);
        if (entity is null || entity.CompanyId != command.CompanyId)
            return Result.Failure<WorkflowRequest>(CostOverErrors.CostOverWithIdNotFound);

        if (entity.CreatorId != command.UserId)
            return Result.Failure<WorkflowRequest>(CostOverErrors.ApprovalCreatorRequired);

        var businessKey = entity.Id.ToString(CultureInfo.InvariantCulture);
        var existing = await _workflows.GetOpen(command.CompanyId, CostOverWorkflowContract.EntityType,
            businessKey, CostOverWorkflowContract.ApprovalPurpose, ct);
        if (existing is not null)
        {
            if (entity.ApprovalStatus == CostOverApprovalStatus.PendingApproval &&
                entity.CurrentApprovalAttemptId == existing.RequestId) return existing;
            return Result.Failure<WorkflowRequest>(CostOverErrors.ApprovalConflict);
        }

        if (entity.ApprovalStatus != CostOverApprovalStatus.Draft)
            return Result.Failure<WorkflowRequest>(CostOverErrors.ApprovalConflict);

        var pair = WorkflowRequestFactory.Create(command.CompanyId, CostOverWorkflowContract.EntityType,
            businessKey, CostOverWorkflowContract.ApprovalPurpose, CostOverWorkflowContract.ApprovalWorkflowCode,
            command.UserId, new { entity.Id, entity.CostOverCode, entity.CostOverName, entity.CompanyId, entity.IsActive });

        entity.SubmitForApproval(pair.Request.RequestId);
        await _costOvers.Update(entity);
        _workflows.Add(pair.Request, pair.Outbox);
        return pair.Request;
    }
}
