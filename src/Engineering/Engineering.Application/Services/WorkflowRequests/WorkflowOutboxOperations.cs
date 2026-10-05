using Gita.Backend.Shared.Domain.Base;
using Engineering.Application.Abstractions.Data.WorkflowRequests;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.WorkflowRequests;

/// <summary>اطلاعات مدیریتی ارسال؛ شامل Payload و توکن نیست.</summary>
public sealed record WorkflowDispatchFailureResponse(
    Guid MessageId, Guid RequestId, int Attempts, bool IsSuspended,
    DateTime NextAttemptAtUtc, DateTime? LockedUntilUtc, string? LastError);

/// <summary>ارسال مجدد همان پیام؛ دلیل برای پیگیری اقدام مدیر الزامی است.</summary>
public sealed record RetryWorkflowMessageRequest(Guid MessageId, string Reason);

/// <summary>پیگیری و ارسال مجدد پیام های همان شرکت با مجوز صریح workflow.operate.</summary>
public sealed class WorkflowOutboxOperations(
    IWorkflowIntegrationRepository repository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider identity,
    IUserProfileService profiles,
    ILogger<WorkflowOutboxOperations> logger)
{
    private static readonly Error Forbidden = new("WorkflowOperateRequired", "مجوز مدیریت ارسال گردش کار الزامی است.", 403);

    /// <summary>پیام های نیازمند پیگیری را بدون نمایش اطلاعات کسب و کار یا اسرار صفحه بندی می کند.</summary>
    public async Task<Result<List<WorkflowDispatchFailureResponse>>> Failures(int page, int size, CT ct)
    {
        if (!CanOperate()) return Result.Failure<List<WorkflowDispatchFailureResponse>>(Forbidden);
        if (page < 1 || page > 1000000 || size < 1 || size > 100)
            return Result.Failure<List<WorkflowDispatchFailureResponse>>(
                new Error("InvalidPaging", "صفحه بندی معتبر نیست.", 400));

        var messages = await repository.GetFailures(identity.CompanyId, page, size, ct);
        return Result.Success(messages.Select(x => new WorkflowDispatchFailureResponse(
            x.MessageId, x.RequestId, x.Attempts, x.IsSuspended,
            x.NextAttemptAtUtc, x.LockedUntilUtc, x.LastError)).ToList());
    }

    /// <summary>پس از کنترل شرکت و نبود اجاره فعال، بودجه همان پیام را تازه می کند؛ شبکه در این درخواست فراخوانی نمی شود.</summary>
    public async Task<Result<bool>> Retry(RetryWorkflowMessageRequest input, CT ct)
    {
        if (!CanOperate()) return Result.Failure<bool>(Forbidden);
        if (input.MessageId == Guid.Empty || string.IsNullOrWhiteSpace(input.Reason) || input.Reason.Length > 500)
            return Result.Failure<bool>(new Error("InvalidRetry", "شناسه پیام و دلیل حداکثر 500 نویسه ای الزامی است.", 400));

        var message = await repository.GetMessage(input.MessageId, ct);
        if (message is null || message.CompanyId != identity.CompanyId)
            return Result.Failure<bool>(new Error("WorkflowMessageNotFound", "پیام یافت نشد.", 404));
        var request = await repository.Get(identity.CompanyId, message.RequestId, ct);
        if (request is null || !request.IsOpen || request.FinishedAtUtc.HasValue)
            return Result.Failure<bool>(new Error("WorkflowRetryConflict", "درخواست بسته یا پایان یافته قابل ارسال مجدد نیست.", 409));

        var now = DateTime.UtcNow;
        if (message.ProcessedAtUtc.HasValue || message.LockedUntilUtc > now ||
            (!message.IsSuspended && string.IsNullOrWhiteSpace(message.LastError)))
            return Result.Failure<bool>(new Error("WorkflowRetryConflict", "پیام در حال پردازش، تحویل شده یا فاقد خطای ارسال است.", 409));

        message.RetryManually(identity.UserId, input.Reason.Trim(), now);
        request.RecordDispatchError(message.LastError!);
        await unitOfWork.CommitAsync(ct);
        logger.LogInformation("Workflow message {MessageId} for company {CompanyId} scheduled by user {UserId}; reason: {Reason}",
            message.MessageId, identity.CompanyId, identity.UserId, input.Reason);
        return Result.Success(true);
    }

    /// <summary>کاربر انسانی و شرکت معتبر به همراه مجوز صریح در scope یا پروفایل لازم است.</summary>
    private bool CanOperate()
    {
        if (identity.CompanyId <= 0 || identity.UserId <= 0) return false;
        if (identity.Scopes?.Contains("workflow.operate", StringComparer.Ordinal) == true) return true;
        var profile = profiles.GetProfileInfo();
        return profile.Actions?.Contains("workflow.operate", StringComparer.Ordinal) == true ||
            profile.Scopes?.Contains("workflow.operate", StringComparer.Ordinal) == true;
    }
}