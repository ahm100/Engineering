
namespace Engineering.Domain.Entities.WorkflowRequests;

/// <summary>
/// توضیحات موجودیت مشترک درخواست گردش کار و ویژگی های آن.
/// </summary>
public static class WorkflowRequestCmts
{
    public const string WorkflowRequest = "درخواست گردش کار";

    public const string RequestId = "شناسه یکتای درخواست در ارتباط بین سرویس ها";
    public const string CompanyId = "شناسه شرکت";
    public const string EntityType = "نوع موجودیت درخواست کننده";
    public const string BusinessKey = "شناسه رکورد کسب و کار";
    public const string Purpose = "هدف اجرای فرایند";
    public const string WorkflowCode = "کد فرایند";
    public const string RequestedByUserId = "شناسه کاربر درخواست کننده";
    public const string VariablesJson = "تصویر ثابت اطلاعات اولیه فرایند";
    public const string WorkflowInstanceId = "شناسه نمونه فرایند در سرویس گردش کار";
    public const string DispatchStatus = "وضعیت ارسال درخواست";
    public const string ExecutionStatus = "آخرین وضعیت دریافت شده از اجرای فرایند";
    public const string Outcome = "نتیجه کسب و کار فرایند";
    public const string RequestedAtUtc = "زمان ایجاد درخواست به وقت UTC";
    public const string AcceptedAtUtc = "زمان پذیرش درخواست توسط سرویس گردش کار به وقت UTC";
    public const string FinishedAtUtc = "زمان پایان اجرای فرایند به وقت UTC";
    public const string LastError = "آخرین خطای ثبت شده در پیگیری درخواست";
    public const string IsOpen = "باز بودن درخواست برای موجودیت و هدف مربوطه";
}

/// <summary>
/// توضیحات پیام های خروجی ارتباط با سرویس گردش کار.
/// </summary>
public static class WorkflowOutboxCmts
{
    public const string WorkflowOutbox = "پیام خروجی گردش کار";
    public const string MessageId = "شناسه یکتای پیام";
    public const string RequestId = "شناسه درخواست گردش کار";
    public const string CompanyId = "شناسه شرکت";
    public const string MessageType = "نوع و نسخه پیام";
    public const string PayloadJson = "محتوای ثابت پیام";
    public const string CreatedAtUtc = "زمان ایجاد پیام به وقت UTC";
    public const string Attempts = "تعداد تلاش های پردازش پیام";
    public const string NextAttemptAtUtc = "زمان مجاز تلاش بعدی به وقت UTC";
    public const string ProcessedAtUtc = "زمان ثبت پذیرش پیام توسط مقصد به وقت UTC";
    public const string LastError = "آخرین خطای پردازش";
    public const string LockId = "شناسه مالک پردازش پیام";
    public const string LockedUntilUtc = "زمان انقضای مالکیت پردازش به وقت UTC";
    public const string IsSuspended = "توقف ارسال خودکار پیام";
    public const string RowVersion = "نسخه رکورد برای کنترل هم زمانی";
}

/// <summary>توضیحات رسید پردازش نتیجه گردش کار.</summary>
public static class WorkflowInboxCmts
{
    public const string WorkflowInbox = "رسید نتیجه گردش کار";
    public const string EventId = "شناسه یکتای رویداد در سرویس گردش کار";
    public const string CompanyId = "شناسه شرکت";
    public const string RequestId = "شناسه درخواست گردش کار در مهندسی";
    public const string PayloadHash = "اثر انگشت محتوای رویداد برای تشخیص تکرار متناقض";
    public const string ProcessedAtUtc = "زمان ثبت نتیجه به وقت UTC";
}
