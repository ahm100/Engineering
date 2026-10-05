using Microsoft.Extensions.DependencyInjection;
using System.Threading.Channels;

namespace Engineering.Application.Extensions.BackgroundTask;

public interface IBackgroundTaskQueue
{
    Task QueueBackgroundWorkItem(Func<IServiceProvider, CancellationToken, Task> workItem);
    Task ProcessQueueAsync(IServiceProvider serviceProvider, CT ct);
}

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly Channel<Func<IServiceProvider, CancellationToken, Task>> _queue =
        Channel.CreateUnbounded<Func<IServiceProvider, CancellationToken, Task>>();

    public async Task QueueBackgroundWorkItem(Func<IServiceProvider, CancellationToken, Task> workItem)
    {
        await _queue.Writer.WriteAsync(workItem);
    }

    public async Task ProcessQueueAsync(IServiceProvider serviceProvider, CT ct)
    {
        await foreach (var workItem in _queue.Reader.ReadAllAsync(ct))
        {
            using var scope = serviceProvider.CreateScope(); // ایجاد یک Scoped Service جدید
            await workItem(scope.ServiceProvider, ct);
        }
    }
}
