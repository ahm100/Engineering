using Engineering.Domain.Entities.WorkflowRequests;

namespace Engineering.Application.Abstractions.Data.WorkflowRequests;

/// <summary>ثبت درخواست و پیام گردش کار در Context مشترک واحد کار.</summary>
public interface IWorkflowIntegrationRepository
{
    /// <summary>درخواست و پیام متناظر را بدون ذخیره نهایی ثبت می کند.</summary>
    void Add(
        WorkflowRequest request,
        WorkflowOutbox outbox);

    /// <summary>درخواست را در محدوده شرکت با Tracking دریافت می کند.</summary>
    Task<WorkflowRequest?> Get(
        long companyId,
        Guid requestId,
        CT ct);

    /// <summary>درخواست باز موجودیت و هدف مشخص را دریافت می کند.</summary>
    Task<WorkflowRequest?> GetOpen(
        long companyId,
        string entityType,
        string businessKey,
        string purpose,
        CT ct);

    /// <summary>قدیمی ترین پیام آماده را با Tracking برای رزرو دریافت می کند.</summary>
    Task<WorkflowOutbox?> GetNext(
        DateTime nowUtc, CT ct);

    /// <summary>پیام رزروشده را برای ثبت نتیجه ارسال دریافت می کند.</summary>
    Task<WorkflowOutbox?> GetMessage(
        Guid messageId, CT ct);

    /// <summary>درخواست متناظر با نمونه مقصد را در شرکت مشخص دریافت می کند.</summary>
    Task<WorkflowRequest?> GetByInstance(
        long companyId,
        long instanceId, CT ct);

    /// <summary>رسید رویداد را برای تشخیص تکرار دریافت می کند.</summary>
    Task<WorkflowInbox?> GetReceipt(
        long eventId, CT ct);

    /// <summary>پیام های ناموفق یا متوقف شده همان شرکت را برای پیگیری مدیر صفحه بندی می کند.</summary>
    Task<List<WorkflowOutbox>> GetFailures(long companyId, int page, int size, CT ct);

    /// <summary>رسید پردازش نتیجه را بدون Commit ثبت می کند.</summary>
    void AddReceipt(WorkflowInbox receipt);
}
