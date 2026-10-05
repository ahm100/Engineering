namespace Engineering.Infra.WorkflowRequests;

/// <summary>تنظیمات اتصال REST و دریافت نتیجه؛ اسرار از تنظیمات محیطی تأمین می شوند.</summary>
public sealed class WorkflowIntegrationOptions
{
    public const string SectionName = "WorkflowIntegration";
    public bool Enabled { get; set; } = true;
    public string BaseUrl { get; set; } = "http://localhost:8081/";
    public string StartPath { get; set; } = "api/workflow/v1/instances/StartWorkflow";
    public string CallbackSecret { get; set; } = string.Empty;
    public int PollIntervalSeconds { get; set; } = 2;
    /// <summary>حداکثر تعداد تلاش در هر دوره ارسال خودکار.</summary>
    public int MaxAttempts { get; set; } = 8;
    /// <summary>فاصله اولیه ارسال مجدد؛ زمان با هر شکست افزایش می یابد.</summary>
    public int RetryBaseSeconds { get; set; } = 5;
    /// <summary>سقف فاصله تلاش های خودکار.</summary>
    public int RetryMaxSeconds { get; set; } = 300;
    /// <summary>مهلت دریافت توکن، ارسال و ثبت پاسخ؛ کمتر از اجاره دو دقیقه ای نگه داشته می شود.</summary>
    public int DispatchTimeoutSeconds { get; set; } = 60;
}
