
namespace Engineering.Domain.Entities.WorkflowRequests;

/// <summary>رسید پایدار پیام نتیجه برای جلوگیری از پردازش تکراری یا متناقض.</summary>
[Description(WorkflowInboxCmts.WorkflowInbox)]
public sealed class WorkflowInbox : AuditableEntity<WorkflowInbox, long>
{
    [Description(WorkflowInboxCmts.EventId)]
    public long EventId { get; private set; }

    [Description(WorkflowInboxCmts.CompanyId)]
    public long CompanyId { get; private set; }

    [Description(WorkflowInboxCmts.RequestId)]
    public Guid RequestId { get; private set; }

    [Description(WorkflowInboxCmts.PayloadHash)]
    public string PayloadHash { get; private set; } = string.Empty;

    [Description(WorkflowInboxCmts.ProcessedAtUtc)]
    public DateTime ProcessedAtUtc { get; private set; }

    /// <summary>رسید نتیجه را برای ذخیره همراه تغییر موجودیت ایجاد می کند.</summary>
    public WorkflowInbox(
        long eventId,
        long companyId,
        Guid requestId,
        string payloadHash,
        DateTime nowUtc)
    {
        if (eventId <= 0 || companyId <= 0 || requestId == Guid.Empty || payloadHash.Length != 64 || nowUtc.Kind != DateTimeKind.Utc)
            throw new InvalidOperationException("رسید نتیجه معتبر نیست.");

        EventId = eventId;
        CompanyId = companyId;
        RequestId = requestId;
        PayloadHash = payloadHash;
        ProcessedAtUtc = nowUtc;
        // نتیجه توسط سرویس ثبت می شود؛ کاربر درخواست کننده، عامل ثبت این رسید نیست.
        CheckUser = true;
        CreatorId = 0;
    }

    /// <summary>سازنده مخصوص EF Core.</summary>
    private WorkflowInbox() { }
}
