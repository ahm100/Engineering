using Engineering.Application.Services.WorkflowRequests;
using Microsoft.EntityFrameworkCore;

namespace Engineering.Api.Controllers.WorkflowRequests;

/// <summary>مدیریت پیام های خروجی؛ مستقل از endpoint ناشناس Callback و نیازمند احراز هویت است.</summary>
[ApiController]
[Authorize]
[Route("api/engineering/v1/workflow/operations")]
public sealed class WorkflowOutboxOperationsController(WorkflowOutboxOperations operations) : ControllerBase
{
    /// <summary>پیام های خطادار یا متوقف شده شرکت کاربر دارای workflow.operate را دریافت می کند.</summary>
    [HttpGet("GetFailures")]
    [ResponseSchema<List<WorkflowDispatchFailureResponse>>]
    public async Task<IResult> GetFailures(CT ct, [FromQuery] int page = 1, [FromQuery] int size = 20) =>
        (await operations.Failures(page, size, ct)).GetHttpResponse();

    /// <summary>همان MessageId و Payload را پس از رفع علت خطا برای ارسال مجدد توسط Worker آماده می کند.</summary>
    [HttpPost("RetryMessage")]
    [ResponseSchema<bool>]
    public async Task<IResult> RetryMessage([FromBody] RetryWorkflowMessageRequest request, CT ct)
    {
        try
        {
            return (await operations.Retry(request, ct)).GetHttpResponse();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Results.Conflict(new { Message = "وضعیت پیام هم زمان تغییر کرد؛ وضعیت جدید را دریافت کنید." });
        }
    }
}