using Engineering.Application.Services.WorkflowRequests;
using Engineering.Infra.WorkflowRequests;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Engineering.Api.Controllers.WorkflowRequests;

[ApiController]
[Route("api/engineering/v1/workflow")]
public sealed class WorkflowIntegrationController(ILogger<WorkflowIntegrationController> logger) : ControllerBase
{
    /// <summary>وضعیت درخواست گردش کار را برای شرکت هویت جاری برمی گرداند.</summary>
    [HttpGet("requests/{requestId:guid}")]
    public async Task<IActionResult> GetStatus(Guid requestId,
        [FromServices] IdentityServer.ClientSdk.Services.IUserInfoProvider identity,
        [FromServices] WorkflowIntegrationLogic logic, CT ct)
    {
        var status = await logic.GetStatus(identity.CompanyId, requestId, ct);
        return status is null ? NotFound() : Ok(status);
    }

    /// <summary>نتیجه امضاشده Workflow را دریافت و از مسیر Logic مشترک اعمال می کند.</summary>
    [HttpPost("result")]
    [AllowAnonymous]
    [RequestSizeLimit(65536)]
    public async Task<IActionResult> Result(
        [FromServices] WorkflowCallbackVerifier verifier,
        [FromServices] WorkflowIntegrationLogic logic, CT ct)
    {
        if (!verifier.IsConfigured)
            return Failure(503, "Callback.Configuration", "تنظیمات دریافت نتیجه تکمیل نشده است.");

        using var reader = new StreamReader(
            Request.Body, 
            new UTF8Encoding(false, true), 
            false, 
            4096, 
            true);

        string body;
        try
        {
            body = await reader.ReadToEndAsync(ct);
        }
        catch (DecoderFallbackException)
        {
            return Failure(400, "Callback.InvalidPayload", "محتوای پیام نتیجه معتبر نیست.");
        }

        var eventId = Request.Headers["X-Workflow-Event-Id"].ToString();
        if (!verifier.Verify(eventId, Request.Headers["X-Workflow-Timestamp"].ToString(), 
            Request.Headers["X-Workflow-Signature"].ToString(), body))
            return Failure(401, "Callback.Signature", "امضا یا زمان پیام معتبر نیست.");

        WorkflowResultMessage? message;
        try
        {
            message = JsonSerializer.Deserialize<WorkflowResultMessage>(body, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
        catch (JsonException)
        {
            return Failure(400, "Callback.InvalidPayload", "محتوای پیام نتیجه معتبر نیست.");
        }

        if (message is null || message.EventId.ToString(CultureInfo.InvariantCulture) != eventId)
            return Failure(400, "Callback.InvalidPayload", "محتوای پیام نتیجه معتبر نیست.");

        try
        {
            await logic.ApplyResult(message, ct);
        }
        catch (InvalidOperationException exception)
        {
            logger.LogWarning(exception, "Callback conflict. EventId={EventId}, RequestId={RequestId}, CompanyId={CompanyId}, TraceId={TraceId}", message.EventId, message.RequestId, message.CompanyId, HttpContext.TraceIdentifier);
            return Failure(409, "Callback.Conflict", "نتیجه با درخواست ثبت شده سازگار نیست؛ لاگ مقصد بررسی شود.");
        }
        catch (DbUpdateConcurrencyException exception)
        {
            logger.LogWarning(exception, "Callback concurrency. EventId={EventId}, TraceId={TraceId}", message.EventId, HttpContext.TraceIdentifier);
            return Failure(409, "Callback.Concurrency", "ثبت نتیجه با پردازش هم زمان تداخل داشت؛ ارسال تکرار شود.");
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Callback failed. EventId={EventId}, RequestId={RequestId}, CompanyId={CompanyId}, TraceId={TraceId}", message.EventId, message.RequestId, message.CompanyId, HttpContext.TraceIdentifier);
            return Failure(500, exception is DbUpdateException ? "Callback.Storage" : "Callback.Internal", "ثبت نتیجه ناموفق بود؛ علت اصلی در لاگ مقصد ثبت شده است.");
        }

        return Ok();
    }
    /// <summary>خطای امن را در قالب مشترک و شناسه پیگیری را در هدر پاسخ برمی گرداند.</summary>
    private ObjectResult Failure(int status, string code, string message)
    {
        Response.Headers["X-Correlation-Id"] = HttpContext.TraceIdentifier;
        return StatusCode(status, new { isSuccess = false, isFailure = true, error = new { code, message, statusCode = status } });
    }}

