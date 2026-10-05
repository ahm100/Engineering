using Engineering.Domain.Entities.WorkflowRequests.Contracts;
using Engineering.Domain.Entities.WorkflowRequests.Enums;
using System.Text.Json;

namespace Engineering.Domain.Entities.WorkflowRequests;

[Description(WorkflowOutboxCmts.WorkflowOutbox)]
public class WorkflowOutbox : AuditableEntity<WorkflowOutbox, long>
{
    [Description(WorkflowOutboxCmts.MessageId)]
    public Guid MessageId { get; private set; }

    [Description(WorkflowOutboxCmts.RequestId)]
    public Guid RequestId { get; private set; }

    [Description(WorkflowOutboxCmts.CompanyId)]
    public long CompanyId { get; private set; }

    [Description(WorkflowOutboxCmts.MessageType)]
    public string MessageType { get; private set; } = string.Empty;

    [Description(WorkflowOutboxCmts.PayloadJson)]
    public string PayloadJson { get; private set; } = "{}";

    [Description(WorkflowOutboxCmts.CreatedAtUtc)]
    public DateTime CreatedAtUtc { get; private set; }

    [Description(WorkflowOutboxCmts.Attempts)]
    public int Attempts { get; private set; }

    [Description(WorkflowOutboxCmts.NextAttemptAtUtc)]
    public DateTime NextAttemptAtUtc { get; private set; }

    [Description(WorkflowOutboxCmts.ProcessedAtUtc)]
    public DateTime? ProcessedAtUtc { get; private set; }

    [Description(WorkflowOutboxCmts.LastError)]
    public string? LastError { get; private set; }

    [Description(WorkflowOutboxCmts.LockId)]
    public Guid? LockId { get; private set; }

    [Description(WorkflowOutboxCmts.LockedUntilUtc)]
    public DateTime? LockedUntilUtc { get; private set; }

    [Description(WorkflowOutboxCmts.IsSuspended)]
    public bool IsSuspended { get; private set; }

    /// <summary>
    /// پیام شروع را برای درخواست ثبت شده با محتوای ثابت ایجاد می کند.
    /// ذخیره پیام و درخواست باید در یک واحد کار انجام شود.
    /// </summary>
    public static WorkflowOutbox CreateStart(
        WorkflowRequest request,
        string payloadJson,
        DateTime nowUtc)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.RequestId == Guid.Empty || request.CompanyId <= 0)
            throw new InvalidOperationException(
                "شناسه درخواست و شرکت معتبر نیست.");

        if (request.DispatchStatus != WorkflowDispatchStatus.Pending ||
            !request.IsOpen)
        {
            throw new InvalidOperationException(
                "پیام شروع فقط برای درخواست باز و در انتظار ارسال ایجاد می شود.");
        }

        if (nowUtc.Kind != DateTimeKind.Utc)
            throw new InvalidOperationException(
                "زمان ایجاد پیام باید بر اساس UTC باشد.");

        if (string.IsNullOrWhiteSpace(payloadJson))
            throw new InvalidOperationException(
                "محتوای پیام الزامی است.");

        using var document = JsonDocument.Parse(payloadJson);

        if (document.RootElement.ValueKind != JsonValueKind.Object)
            throw new InvalidOperationException(
                "محتوای پیام باید یک شیء JSON باشد.");

        return new WorkflowOutbox
        {
            MessageId = Guid.NewGuid(),
            RequestId = request.RequestId,
            CompanyId = request.CompanyId,
            MessageType = WorkflowOutboxTypes.StartWorkflowV1,
            PayloadJson = payloadJson,
            CreatedAtUtc = nowUtc,
            NextAttemptAtUtc = nowUtc
        };
    }

    /// <summary>پیام آماده را با مهلت محدود برای یک پردازشگر رزرو می کند.</summary>
    public void Claim(Guid lockId, DateTime nowUtc, TimeSpan lease)
    {
        if (lockId == Guid.Empty || nowUtc.Kind != DateTimeKind.Utc || lease <= TimeSpan.Zero ||
            ProcessedAtUtc.HasValue || IsSuspended || NextAttemptAtUtc > nowUtc || LockedUntilUtc > nowUtc)
            throw new InvalidOperationException("پیام برای پردازش آماده نیست.");
        LockId = lockId;
        LockedUntilUtc = nowUtc.Add(lease);
        Attempts++;
        PrepareSystemUpdate();
    }

    /// <summary>پذیرش مقصد را فقط توسط مالک فعلی ثبت می کند.</summary>
    public void Complete(Guid lockId, DateTime nowUtc)
    {
        EnsureOwner(lockId, nowUtc);
        ProcessedAtUtc = nowUtc;
        PrepareSystemUpdate();
        LastError = null;
        LockId = null;
        LockedUntilUtc = null;
    }

    /// <summary>خطای ارسال را ثبت و پیام را برای تلاش بعدی زمان بندی یا برای بررسی مدیر متوقف می کند.</summary>
    public void Retry(Guid lockId, DateTime nowUtc, string error, bool suspend, TimeSpan delay)
    {
        EnsureOwner(lockId, nowUtc);
        if (delay < TimeSpan.Zero || string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("اطلاعات تلاش مجدد معتبر نیست.");
        LastError = error.Length > 2000 ? error[..2000] : error;
        IsSuspended = suspend;
        NextAttemptAtUtc = nowUtc.Add(delay);
        LockId = null;
        LockedUntilUtc = null;
        PrepareSystemUpdate();
    }

    /// <summary>پیام بدون مالک فعال را پس از مصرف بودجه تلاش، حتی در صورت قطع پردازش قبلی متوقف می کند.</summary>
    public void SuspendExhausted(DateTime nowUtc)
    {
        if (nowUtc.Kind != DateTimeKind.Utc || ProcessedAtUtc.HasValue || LockedUntilUtc > nowUtc)
            throw new InvalidOperationException("پیام در حال پردازش یا تکمیل شده است.");
        IsSuspended = true;
        LastError = "[AttemptsExhausted] سقف تلاش خودکار مصرف شده است؛ بررسی و ارسال مجدد مدیریتی لازم است.";
        LockId = null;
        LockedUntilUtc = null;
        PrepareSystemUpdate();
    }

    /// <summary>همان پیام و محتوای ثابت را با ثبت عامل و دلیل برای یک دوره جدید ارسال آماده می کند.</summary>
    public void RetryManually(long userId, string reason, DateTime nowUtc)
    {
        if (userId <= 0 || nowUtc.Kind != DateTimeKind.Utc ||
            string.IsNullOrWhiteSpace(reason) || reason.Length > 500 ||
            ProcessedAtUtc.HasValue || LockedUntilUtc > nowUtc)
            throw new InvalidOperationException("پیام برای ارسال مجدد مدیریتی معتبر نیست.");
        var detail = $"[ManualRetry] User={userId}; Reason={reason}; Previous={LastError}";
        LastError = detail.Length > 2000 ? detail[..2000] : detail;
        Attempts = 0;
        IsSuspended = false;
        NextAttemptAtUtc = nowUtc;
        LockId = null;
        LockedUntilUtc = null;
        CheckUser = true;
        UpdaterId = userId;
    }
    /// <summary>ویرایش Worker را بدون نیاز به پروفایل HTTP به عنوان تغییر سیستمی مشخص می کند.</summary>
    private void PrepareSystemUpdate()
    {
        CheckUser = true;
        UpdaterId = null;
    }

    /// <summary>مالکیت معتبر و عدم پردازش قبلی پیام را بررسی می کند.</summary>
    private void EnsureOwner(Guid lockId, DateTime nowUtc)
    {
        if (nowUtc.Kind != DateTimeKind.Utc || LockId != lockId || LockedUntilUtc <= nowUtc ||
            ProcessedAtUtc.HasValue)
            throw new InvalidOperationException("مالکیت پردازش پیام معتبر نیست.");
    }

    /// <summary>سازنده مورد استفاده EF Core.</summary>
    private WorkflowOutbox()
    {
    }
}
