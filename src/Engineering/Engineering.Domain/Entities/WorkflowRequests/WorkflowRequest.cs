using Engineering.Domain.Entities.WorkflowRequests.Enums;

namespace Engineering.Domain.Entities.WorkflowRequests;

[Description(WorkflowRequestCmts.WorkflowRequest)]
public class WorkflowRequest : AuditableEntity<WorkflowRequest>
{
    [Description(WorkflowRequestCmts.RequestId)]
    public Guid RequestId { get; private set; }

    [Description(WorkflowRequestCmts.CompanyId)]
    public long CompanyId { get; private set; }

    [Description(WorkflowRequestCmts.EntityType)]
    public string EntityType { get; private set; } = string.Empty;

    [Description(WorkflowRequestCmts.BusinessKey)]
    public string BusinessKey { get; private set; } = string.Empty;

    [Description(WorkflowRequestCmts.Purpose)]
    public string Purpose { get; private set; } = string.Empty;

    [Description(WorkflowRequestCmts.WorkflowCode)]
    public string WorkflowCode { get; private set; } = string.Empty;

    [Description(WorkflowRequestCmts.RequestedByUserId)]
    public long RequestedByUserId { get; private set; }

    [Description(WorkflowRequestCmts.VariablesJson)]
    public string VariablesJson { get; private set; } = "{}";

    [Description(WorkflowRequestCmts.WorkflowInstanceId)]
    public long? WorkflowInstanceId { get; private set; }

    [Description(WorkflowRequestCmts.DispatchStatus)]
    public WorkflowDispatchStatus DispatchStatus { get; private set; }
        = WorkflowDispatchStatus.Pending;

    [Description(WorkflowRequestCmts.ExecutionStatus)]
    public WorkflowExecutionStatus ExecutionStatus { get; private set; }
        = WorkflowExecutionStatus.Unknown;

    [Description(WorkflowRequestCmts.Outcome)]
    public string? Outcome { get; private set; }

    [Description(WorkflowRequestCmts.RequestedAtUtc)]
    public DateTime RequestedAtUtc { get; private set; }

    [Description(WorkflowRequestCmts.AcceptedAtUtc)]
    public DateTime? AcceptedAtUtc { get; private set; }

    [Description(WorkflowRequestCmts.FinishedAtUtc)]
    public DateTime? FinishedAtUtc { get; private set; }

    [Description(WorkflowRequestCmts.LastError)]
    public string? LastError { get; private set; }

    /// <summary>
    /// پایان اجرا یا خطا به تنهایی باعث آزاد شدن جایگاه درخواست جاری نمی شود.
    /// </summary>
    [Description(WorkflowRequestCmts.IsOpen)]
    public bool IsOpen { get; private set; } = true;

    /// <summary>
    /// درخواست جدید را با شناسه ثابت و تصویر اطلاعات زمان ارسال ایجاد می کند.
    /// </summary>
    public WorkflowRequest(
        Guid requestId,
        long companyId,
        string entityType,
        string businessKey,
        string purpose,
        string workflowCode,
        long requestedByUserId,
        string variablesJson,
        DateTime requestedAtUtc)
    {
        if (requestId == Guid.Empty)
            throw new InvalidOperationException("شناسه درخواست معتبر نیست.");

        if (companyId <= 0 || requestedByUserId <= 0)
            throw new InvalidOperationException(
                "شناسه شرکت و کاربر درخواست کننده باید معتبر باشند.");

        if (requestedAtUtc.Kind != DateTimeKind.Utc)
            throw new InvalidOperationException(
                "زمان درخواست باید بر اساس UTC باشد.");

        RequestId = requestId;
        CompanyId = companyId;
        RequestedByUserId = requestedByUserId;

        EntityType = ValidateText(entityType, 100, nameof(entityType));
        BusinessKey = ValidateText(businessKey, 200, nameof(businessKey));
        Purpose = ValidateText(purpose, 100, nameof(purpose));
        WorkflowCode = ValidateText(workflowCode, 100, nameof(workflowCode));

        if (string.IsNullOrWhiteSpace(variablesJson))
            throw new InvalidOperationException("اطلاعات فرایند الزامی است.");

        using var document = System.Text.Json.JsonDocument.Parse(variablesJson);

        if (document.RootElement.ValueKind !=
            System.Text.Json.JsonValueKind.Object)
        {
            throw new InvalidOperationException(
                "اطلاعات فرایند باید یک شیء JSON باشد.");
        }

        VariablesJson = variablesJson;
        RequestedAtUtc = requestedAtUtc;
    }

    /// <summary>
    /// مقدار متنی الزامی را بدون تغییر محتوای شناسه بررسی می کند.
    /// </summary>
    private static string ValidateText(
        string value,
        int maxLength,
        string field)
    {
        var result = Guard.Against.NullOrWhiteSpace(value, field);

        if (result.Length > maxLength)
            throw new InvalidOperationException(
                $"طول مقدار {field} بیشتر از حد مجاز است.");

        return result;
    }

    /// <summary>پذیرش مقصد را بدون تغییر نتیجه قطعی قبلی ثبت می کند.</summary>
    public void Accept(long instanceId, DateTime nowUtc)
    {
        if (instanceId <= 0 || nowUtc.Kind != DateTimeKind.Utc ||
            (WorkflowInstanceId.HasValue && WorkflowInstanceId != instanceId))
            throw new InvalidOperationException("پذیرش فرایند با درخواست سازگار نیست.");
        WorkflowInstanceId = instanceId;
        DispatchStatus = WorkflowDispatchStatus.Accepted;
        AcceptedAtUtc ??= nowUtc;
        LastError = null;
        PrepareSystemUpdate();
    }

    /// <summary>خطای ارسال را برای پیگیری ذخیره می کند و درخواست را باز نگه می دارد.</summary>
    public void RecordDispatchError(string error)
    {
        LastError = error.Length > 2000 ? error[..2000] : error;
        PrepareSystemUpdate();
    }

    /// <summary>نتیجه نهایی را پس از اعمال قواعد موجودیت ثبت می کند؛ شکست یا لغو جایگاه را آزاد نمی کند.</summary>
    public void Finish(string outcome, DateTime nowUtc)
    {
        if (DispatchStatus != WorkflowDispatchStatus.Accepted || nowUtc.Kind != DateTimeKind.Utc ||
            outcome is not ("Approved" or "Rejected" or "Cancelled" or "Failed"))
            throw new InvalidOperationException("نتیجه نهایی معتبر نیست.");
        if (FinishedAtUtc.HasValue)
        {
            if (Outcome != outcome) throw new InvalidOperationException("نتیجه متناقض دریافت شد.");
            return;
        }
        Outcome = outcome;
        ExecutionStatus = outcome switch
        {
            "Cancelled" => WorkflowExecutionStatus.Cancelled,
            "Failed" => WorkflowExecutionStatus.Faulted,
            _ => WorkflowExecutionStatus.Completed
        };
        FinishedAtUtc = nowUtc;
        PrepareSystemUpdate();
    }

    /// <summary>فقط شکست یا لغو نهایی دریافت شده، قابلیت بستن تلاش و آماده سازی ارسال جدید دارد.</summary>
    public bool CanRecoverExecution() =>
        IsOpen && DispatchStatus == WorkflowDispatchStatus.Accepted &&
        FinishedAtUtc.HasValue &&
        ((Outcome == "Failed" && ExecutionStatus == WorkflowExecutionStatus.Faulted) ||
         (Outcome == "Cancelled" && ExecutionStatus == WorkflowExecutionStatus.Cancelled));

    /// <summary>تلاش شکست خورده یا لغوشده را با حفظ نتیجه و تاریخچه برای ارسال جدید می بندد.</summary>
    public void CloseInterrupted()
    {
        if (!CanRecoverExecution())
            throw new InvalidOperationException("فقط تلاش شکست خورده یا لغوشده نهایی قابل بستن است.");
        IsOpen = false;
    }
    /// <summary>فقط درخواست ردشده را هنگام بازگشت موجودیت به پیش نویس می بندد.</summary>
    public void CloseRejected()
    {
        if (Outcome != "Rejected" || !FinishedAtUtc.HasValue)
            throw new InvalidOperationException("درخواست ردشده نهایی نیست.");
        IsOpen = false;
    }

    /// <summary>تغییر سیستمی را بدون وابستگی به پروفایل HTTP مشخص می کند.</summary>
    private void PrepareSystemUpdate()
    {
        CheckUser = true;
        UpdaterId = null;
    }

    /// <summary>
    /// سازنده مورد استفاده EF Core.
    /// </summary>
    private WorkflowRequest()
    {
    }
}
