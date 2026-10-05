using Engineering.Application.Abstractions.Data.CostOvers;
using Engineering.Application.Services.WorkflowRequests;
using Engineering.Domain.Entities.CostOvers;
using Engineering.Domain.Entities.WorkflowRequests;
using Engineering.Domain.Entities.WorkflowRequests.Contracts;
using System.Globalization;

namespace Engineering.Application.Services.CostOvers;

/// <summary>قواعد اختصاصی اعمال نتیجه تأیید هزینه بالاسری.</summary>
public sealed class CostOverWorkflowHandler : IWorkflowEntityHandler
{
    private readonly ICostOverRepository _repository;
    public string EntityType => CostOverWorkflowContract.EntityType;
    public string Purpose => CostOverWorkflowContract.ApprovalPurpose;

    /// <summary>Repository هزینه بالاسری در واحد کار جاری را دریافت می کند.</summary>
    public CostOverWorkflowHandler(ICostOverRepository repository) => _repository = repository;

    /// <summary>نتیجه مدیر را اعمال می کند؛ در شکست یا لغو، وضعیت انتظار تا اقدام صریح RecoverApproval حفظ می شود و نتیجه فنی در WorkflowRequest ثبت می شود.</summary>
    public async Task Apply(WorkflowRequest request, WorkflowResultMessage result, CT ct)
    {
        if (!long.TryParse(request.BusinessKey, NumberStyles.None, CultureInfo.InvariantCulture, out var id))
            throw new InvalidOperationException("شناسه هزینه بالاسری معتبر نیست.");

        var entity = await _repository.GetCostOverById(id, ct);
        if (entity is null || entity.CompanyId != request.CompanyId ||
            entity.CurrentApprovalAttemptId != request.RequestId || entity.ApprovalStatus != CostOverApprovalStatus.PendingApproval)
            throw new InvalidOperationException("نتیجه متعلق به تلاش جاری هزینه بالاسری نیست.");
         
        if (result.Result is "Approved" or "Rejected")
        {
            entity.ApplyApprovalResult(request.RequestId, result.Result == "Approved");
            entity.CheckUser = true;
            entity.UpdaterId = null;
            await _repository.Update(entity);
        }
    }
}
