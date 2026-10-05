using Engineering.Application.Services.WorkflowRequests;
using Engineering.Domain.Entities.WorkflowRequests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Engineering.Infra.WorkflowRequests;

/// <summary>ارسال پیام مشترک با اجاره محدود، تلاش های محدود و توقف خطای نیازمند بررسی.</summary>
public sealed class WorkflowOutboxWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopes;
    private readonly IOptionsMonitor<WorkflowIntegrationOptions> _options;
    private readonly ILogger<WorkflowOutboxWorker> _logger;

    /// <summary>کارخانه Scope، تنظیمات دوره ارسال و ثبت خطا را دریافت می کند.</summary>
    public WorkflowOutboxWorker(IServiceScopeFactory scopes, IOptionsMonitor<WorkflowIntegrationOptions> options,
        ILogger<WorkflowOutboxWorker> logger)
    {
        _scopes = scopes;
        _options = options;
        _logger = logger;
    }

    /// <summary>پیام های آماده را تا توقف سرویس پردازش می کند؛ Enabled پردازش خودکار را کنترل می کند.</summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_options.CurrentValue.Enabled && await DispatchOne(stoppingToken))
                    continue;
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Workflow Outbox iteration failed.");
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(Math.Clamp(_options.CurrentValue.PollIntervalSeconds, 1, 60)), stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
        }
    }

    /// <summary>توکن، ارسال و ثبت پاسخ را پیش از انقضای اجاره محدود می کند؛ شکست در Scope تازه ذخیره می شود.</summary>
    private async Task<bool> DispatchOne(CancellationToken ct)
    {
        var settings = _options.CurrentValue;
        var maxAttempts = Math.Clamp(settings.MaxAttempts, 1, 100);
        WorkflowOutbox? message;
        await using (var scope = _scopes.CreateAsyncScope())
            message = await scope.ServiceProvider.GetRequiredService<WorkflowIntegrationLogic>().Claim(maxAttempts, ct);
        if (message is null) return false;

        try
        {
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
            timeout.CancelAfter(TimeSpan.FromSeconds(Math.Clamp(settings.DispatchTimeoutSeconds, 5, 90)));
            WorkflowStartAcceptance acceptance;
            await using (var scope = _scopes.CreateAsyncScope())
                acceptance = await scope.ServiceProvider.GetRequiredService<IWorkflowTransport>().Start(message, timeout.Token);

            await using var resultScope = _scopes.CreateAsyncScope();
            await resultScope.ServiceProvider.GetRequiredService<WorkflowIntegrationLogic>()
                .Accept(message.MessageId, message.LockId!.Value, acceptance, timeout.Token);
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested) { throw; }
        catch (Exception exception)
        {
            var failure = WorkflowDispatchFailure.Classify(exception);
            var suspend = !failure.IsTransient || message.Attempts >= maxAttempts;
            var error = message.Attempts >= maxAttempts
                ? "[AttemptsExhausted] " + failure.Message : failure.Message;
            var baseSeconds = Math.Clamp(settings.RetryBaseSeconds, 1, 3600);
            var maxSeconds = Math.Clamp(settings.RetryMaxSeconds, baseSeconds, 86400);
            var delay = TimeSpan.FromSeconds(Math.Min(maxSeconds,
                baseSeconds * Math.Pow(2, Math.Min(Math.Max(message.Attempts - 1, 0), 16))));
            _logger.LogWarning(exception, "Workflow dispatch failed for {MessageId}; suspended={Suspended}.",
                message.MessageId, suspend);
            using var recordTimeout = CancellationTokenSource.CreateLinkedTokenSource(ct);
            recordTimeout.CancelAfter(TimeSpan.FromSeconds(15));
            await using var scope = _scopes.CreateAsyncScope();
            await scope.ServiceProvider.GetRequiredService<WorkflowIntegrationLogic>()
                .Retry(message.MessageId, message.LockId!.Value, error, suspend, delay, recordTimeout.Token);
        }
        return true;
    }
}