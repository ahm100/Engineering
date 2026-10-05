using Engineering.Domain.Entities.WorkflowRequests.Enums;

namespace Engineering.Application.Services.CostOvers.Models.Approval;

/// <summary>شناسه هزینه بالاسری برای عملیات تأیید.</summary>
public sealed record CostOverApprovalRequest(long Id);

/// <summary>رسید ثبت درخواست و وضعیت ارسال به Workflow.</summary>
public sealed record CostOverApprovalResponse(
    long Id,
    Guid RequestId,
    WorkflowDispatchStatus DispatchStatus);


/// <summary>شناسه هزینه بالاسری برای عملیات تأیید.</summary>
public sealed record SetPendingApprovalRequest(long Id);

/// <summary>رسید ثبت درخواست و وضعیت ارسال به Workflow.</summary>
public sealed record SetPendingApprovalResponse(
    long Id);

