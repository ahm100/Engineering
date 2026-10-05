using Engineering.Application.Abstractions.Data.WorkflowRequests;
using Engineering.Domain.Entities.WorkflowRequests;
using System.Security.Cryptography;
using System.Text;

namespace Engineering.Application.Services.WorkflowRequests;

/// <summary>هماهنگی پردازش Outbox و نتیجه؛ تمام Commitها در Logic قرار دارند.</summary>
public sealed class WorkflowIntegrationLogic
{
    private readonly IWorkflowIntegrationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<IWorkflowEntityHandler> _handlers;

    /// <summary>وابستگی های واحد کار و Handlerهای اختصاصی موجودیت را دریافت می کند.</summary>
    public WorkflowIntegrationLogic(
        IWorkflowIntegrationRepository repository,
        IUnitOfWork unitOfWork,
        IEnumerable<IWorkflowEntityHandler> handlers)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _handlers = handlers;
    }

    /// <summary>وضعیت پیگیری را فقط در محدوده شرکت احرازشده دریافت می کند.</summary>
    public async Task<object?> GetStatus(
        long companyId, Guid requestId, CT ct)
    {
        if (companyId <= 0 || requestId == Guid.Empty) return null;
        var request = await _repository.Get(companyId, requestId, ct);

        return request is null ? null : new
        {
            request.RequestId,
            request.EntityType,
            request.BusinessKey,
            request.Purpose,
            request.WorkflowInstanceId,
            request.DispatchStatus,
            request.ExecutionStatus,
            request.Outcome,
            request.IsOpen,
            CanRecoverExecution = request.CanRecoverExecution(),
            request.RequestedAtUtc,
            request.AcceptedAtUtc,
            request.FinishedAtUtc,
            request.LastError
        };
    }

    /// <summary>یک پیام را با رزرو دو دقیقه ای و کنترل هم زمانی ذخیره می کند.</summary>
    public async Task<WorkflowOutbox?> Claim(int maxAttempts, CT ct)
    {
        var now = DateTime.UtcNow;
        var message = await _repository.GetNext(now, ct);
        if (message is null) return null;

        if (maxAttempts < 1) throw new ArgumentOutOfRangeException(nameof(maxAttempts));
        
        if (message.Attempts >= maxAttempts)
        {
            message.SuspendExhausted(now);
            var exhaustedRequest = await _repository.Get(message.CompanyId, message.RequestId, ct);
            exhaustedRequest?.RecordDispatchError(message.LastError!);
            await _unitOfWork.CommitAsync(ct);
            return null;
        }

        message.Claim(Guid.NewGuid(), now, TimeSpan.FromMinutes(2));
        await _unitOfWork.CommitAsync(ct);
        return message;
    }

    /// <summary>پذیرش مقصد و تکمیل Outbox را با یک Commit و مالکیت فعلی ثبت می کند.</summary>
    public async Task Accept(Guid messageId, Guid lockId, WorkflowStartAcceptance acceptance, CT ct)
    {
        var message = await _repository.GetMessage(messageId, ct)
            ?? throw new InvalidOperationException("پیام خروجی پیدا نشد.");

        var request = await _repository.Get(message.CompanyId, message.RequestId, ct)
            ?? throw new InvalidOperationException("درخواست گردش کار پیدا نشد.");

        if (acceptance.BusinessKey != request.BusinessKey || acceptance.Id <= 0 ||
            (acceptance.ClientRequestId.HasValue && acceptance.ClientRequestId != request.RequestId))
            throw new InvalidOperationException("پاسخ مقصد با درخواست شروع سازگار نیست.");

        var now = DateTime.UtcNow;
        message.Complete(lockId, now);
        request.Accept(acceptance.Id, now);
        await _unitOfWork.CommitAsync(ct);
    }

    /// <summary>خطای ارسال و زمان تلاش مجدد را در یک واحد کار ثبت می کند.</summary>
    public async Task Retry(Guid messageId, Guid lockId, string error, bool suspend, TimeSpan delay, CT ct)
    {
        var message = await _repository.GetMessage(messageId, ct);
        var now = DateTime.UtcNow;
        if (message is null || message.ProcessedAtUtc.HasValue || message.LockId != lockId ||
            message.LockedUntilUtc <= now) return;

        var request = await _repository.Get(message.CompanyId, message.RequestId, ct);
        message.Retry(lockId, now, error, suspend, delay);
        request?.RecordDispatchError(error);
        await _unitOfWork.CommitAsync(ct);
    }

    /// <summary>نتیجه امضاشده را با شناسه تلاش یا قرارداد قدیمی تطبیق و پذیرش زودرس، رسید و تغییر موجودیت را با یک Commit ذخیره می کند.</summary>
    public async Task ApplyResult(WorkflowResultMessage result, CT ct)
    {
        if (result.EventId <= 0 || result.CompanyId <= 0 || result.RequestId <= 0 || result.DefinitionVersionId <= 0 ||
            result.ClientRequestId == Guid.Empty ||
            string.IsNullOrWhiteSpace(result.BusinessKey) || result.Result is not ("Approved" or "Rejected" or "Cancelled" or "Failed"))
            throw new InvalidOperationException("پیام نتیجه معتبر نیست.");

        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(
            System.Text.Json.JsonSerializer.Serialize(result))));
        var receipt = await _repository.GetReceipt(result.EventId, ct);
        if (receipt is not null)
        {
            if (receipt.CompanyId != result.CompanyId || receipt.PayloadHash != hash)
                throw new InvalidOperationException("شناسه رویداد با محتوای متفاوت تکرار شده است.");
            return;
        }

        var request = (result.ClientRequestId.HasValue
            ? await _repository.Get(result.CompanyId, result.ClientRequestId.Value, ct)
            : await _repository.GetByInstance(result.CompanyId, result.RequestId, ct))
            ?? throw new InvalidOperationException("درخواست متناظر نتیجه هنوز در Engineering پیدا نشد.");
        if (request.BusinessKey != result.BusinessKey ||
            (request.WorkflowInstanceId.HasValue && request.WorkflowInstanceId != result.RequestId))
            throw new InvalidOperationException("نتیجه با درخواست و نمونه ثبت شده سازگار نیست.");

        request.Accept(result.RequestId, DateTime.UtcNow);

        if (request.FinishedAtUtc.HasValue)
        {
            if (request.Outcome != result.Result)
                throw new InvalidOperationException("نتیجه متناقض دریافت شد.");
        }
        else
        {
            var handler = _handlers.SingleOrDefault(x => x.EntityType == request.EntityType && x.Purpose == request.Purpose)
                ?? throw new InvalidOperationException("Handler موجودیت و هدف ثبت نشده است.");
            await handler.Apply(request, result, ct);
            request.Finish(result.Result, DateTime.UtcNow);
        }

        _repository.AddReceipt(new WorkflowInbox(result.EventId, result.CompanyId, request.RequestId, hash, DateTime.UtcNow));
        await _unitOfWork.CommitAsync(ct);
    }
}
