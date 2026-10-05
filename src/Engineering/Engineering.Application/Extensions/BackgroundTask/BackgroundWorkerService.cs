using Microsoft.Extensions.Hosting;

namespace Engineering.Application.Extensions.BackgroundTask;

public class BackgroundWorkerService : BackgroundService
{
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly IServiceProvider _serviceProvider;

    public BackgroundWorkerService(IBackgroundTaskQueue taskQueue, IServiceProvider serviceProvider)
    {
        _taskQueue = taskQueue;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CT stoppingToken)
    {
        await _taskQueue.ProcessQueueAsync(_serviceProvider, stoppingToken);
    }
}
