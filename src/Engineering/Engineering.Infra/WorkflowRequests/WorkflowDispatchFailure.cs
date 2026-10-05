using System.Net;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;

namespace Engineering.Infra.WorkflowRequests;

/// <summary>تفکیک خطای گذرا از خطای نیازمند بررسی؛ جزئیات حساس استثنا در پیام ذخیره شده قرار نمی گیرد.</summary>
internal sealed record WorkflowDispatchFailure(bool IsTransient, string Message)
{
    /// <summary>خطای ناشناخته را خودکار تکرار نمی کند؛ وضعیت مبهم SDK هویت نیازمند بررسی مدیر است.</summary>
    public static WorkflowDispatchFailure Classify(Exception exception)
    {
        var failure = ClassifyCore(exception);
        return exception.Data["Workflow.Diagnostic"] is string diagnostic
            ? failure with { Message = $"{failure.Message}; {diagnostic}" }
            : failure;
    }

    /// <summary>نوع خطا را مستقل از اطلاعات پیگیری تعیین می کند.</summary>
    private static WorkflowDispatchFailure ClassifyCore(Exception exception)
    {
        if (exception is OperationCanceledException or TimeoutException)
            return new(true, "[Timeout] مهلت ارسال یا دریافت پاسخ پایان یافت.");
        if (exception is DbUpdateConcurrencyException)
            return new(true, "[Concurrency] ثبت پاسخ با پردازش هم زمان تداخل داشت.");
        if (exception is HttpRequestException http)
        {
            if (http.StatusCode is { } status)
            {
                var transient = status is HttpStatusCode.RequestTimeout or HttpStatusCode.TooManyRequests ||
                    (int)status >= 500;
                return new(transient, http is RemoteHttpException ? http.Message : $"[HTTP:{(int)status}] مقصد پاسخ ناموفق داد؛ لاگ بررسی شود.");
            }
            return new(true, "[Connection] اتصال به مقصد برقرار نشد یا ارتباط قطع شد.");
        }
        if (exception is SocketException)
            return new(true, "[Connection] اتصال شبکه برقرار نشد.");
        if (exception is InvalidOperationException or ArgumentException or System.Text.Json.JsonException)
            return new(false, "[ConfigurationOrContract] تنظیمات، محتوای پیام یا قرارداد پاسخ معتبر نیست؛ لاگ بررسی شود.");
        return new(false, "[NeedsReview] علت خطا قابل طبقه بندی نیست؛ لاگ Identity و Engineering بررسی شود.");
    }
}


