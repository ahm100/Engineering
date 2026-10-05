using Engineering.Domain.Entities.WorkflowRequests;
using System.Text.Json;

namespace Engineering.Application.Services.WorkflowRequests;

/// <summary>قرارداد مستقل از روش انتقال برای شروع فرایند.</summary>
public sealed record WorkflowStartPayload(
    string WorkflowCode,
    string BusinessKey,
    JsonElement Variables,
    long OwnerId,
    Guid? ClientRequestId = null);

/// <summary>پاسخ پذیرش شروع؛ پذیرش به معنی پایان موفق موتور نیست.</summary>
public sealed record WorkflowStartAcceptance(
    long Id,
    string BusinessKey,
    Guid? ClientRequestId = null);

/// <summary>قرارداد نتیجه؛ RequestId شناسه عددی مقصد و ClientRequestId شناسه تلاش مهندسی است؛ مقدار تهی برای پیام های قدیمی حفظ می شود.</summary>
public sealed record WorkflowResultMessage(
    long EventId,
    long RequestId,
    long CompanyId,
    string BusinessKey,
    long DefinitionVersionId,
    string Result,
    [property: System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
    Guid? ClientRequestId = null);

/// <summary>درگاه انتقال مشترک برای REST و پیاده سازی آینده RabbitMQ.</summary>
public interface IWorkflowTransport
{
    /// <summary>پیام ثابت را با کلید تکرارپذیری درخواست ارسال می کند.</summary>
    Task<WorkflowStartAcceptance> Start(
        WorkflowOutbox message, CT ct);
}

/// <summary>اعمال نتیجه با قواعد اختصاصی موجودیت، بدون Commit.</summary>
public interface IWorkflowEntityHandler
{
    string EntityType { get; }
    string Purpose { get; }
    /// <summary>نتیجه معتبر را روی تلاش جاری موجودیت اعمال می کند.</summary>
    Task Apply(WorkflowRequest request, WorkflowResultMessage result, CT ct);
}

/// <summary>ساخت درخواست و محتوای ثابت شروع با قرارداد REST فعلی.</summary>
public static class WorkflowRequestFactory
{
    /// <summary>درخواست و پیام شروع را با شناسه مشترک و تصویر ثابت متغیرها می سازد.</summary>
    public static (WorkflowRequest Request, WorkflowOutbox Outbox) Create(
        long companyId,
        string entityType,
        string businessKey,
        string purpose,
        string workflowCode,
        long userId,
        object variables)
    {
        var now = DateTime.UtcNow;
        var variablesJson = System.Text.Json.JsonSerializer.Serialize(variables);
        using var variablesDocument = JsonDocument.Parse(variablesJson);
        var request = new WorkflowRequest(Guid.NewGuid(), companyId, entityType, businessKey, purpose,
            workflowCode, userId, variablesJson, now);

        var payload = new WorkflowStartPayload(workflowCode, businessKey,
            variablesDocument.RootElement, userId, request.RequestId);

        return (request, WorkflowOutbox.CreateStart(request,
            System.Text.Json.JsonSerializer.Serialize(payload), now));
    }
}
