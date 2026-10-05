using Engineering.Domain.Entities.Messages;
using Engineering.Persistence;
using Gita.Backend.Shared.Domain.Exceptions;
using MessageSender.ClientSdk.OutboxPattern;
using MessageSender.ClientSdk.Services;
using Microsoft.EntityFrameworkCore;

namespace Commercial.Api.Services;

public class OutboxStore(EngineeringDBContext context, Func<DateTime> dateTimeProvider)
    : IOutboxStore
{
    public async Task<MessageOutbox> Add(MessageOutbox outbox, CancellationToken cancellationToken)
    {
        var entity = new MessageOutboxEntity
        {
            Envelope = outbox.Envelope,
            Status = MessageOutboxProcessStates.Created,
            QueuedAt = dateTimeProvider(),
        };

        var entry = context.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return new PersistedMessageOutbox
        {
            Envelope = outbox.Envelope,
            Id = entry.Entity.Id,
        };
    }

    public async Task MarkAsSent(MessageOutbox outbox, CancellationToken cancellationToken)
    {
        if (outbox is not PersistedMessageOutbox ob)
        {
            throw new InvalidOperationException("MessageOutbox must be persisted using Add method first.");
        }

        var entity = await context.Set<MessageOutboxEntity>()
            .FirstOrDefaultAsync(x => x.Id == ob.Id, cancellationToken);
        GitaEntityNotFoundException.ThrowIfNull(entity);

        entity.Status = MessageOutboxProcessStates.Sent;
        // entity.BrokerAudit = traceProvider.HostActor;

        context.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task MarkAsError(MessageOutbox outbox, Exception ex, CancellationToken cancellationToken)
    {
        if (outbox is not PersistedMessageOutbox ob)
        {
            throw new InvalidOperationException("MessageOutbox must be persisted using Add method first.");
        }

        var entity = await context.Set<MessageOutboxEntity>()
            .FirstOrDefaultAsync(x => x.Id == ob.Id, cancellationToken);
        GitaEntityNotFoundException.ThrowIfNull(entity);

        entity.Status = MessageOutboxProcessStates.Error;
        entity.ErrorMessage = ex.ToString();
        entity.ErrorRetries++;
        entity.ErrorTime = dateTimeProvider();
        context.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<MessageOutbox[]> GetNextOutboxBatch(CancellationToken cancellationToken)
    {
        var query = context.Set<MessageOutboxEntity>()
                .Where(x => x.Status == MessageOutboxProcessStates.Created ||
                            (x.Status == MessageOutboxProcessStates.Error && x.ErrorRetries < 3));

        var items = await query
            .AsNoTracking()
            .OrderBy(x => x.QueuedAt)
            .Skip(0)
            .Take(10)
            .Select(x => new PersistedMessageOutbox
            {
                Id = x.Id,
                Envelope = x.Envelope
            })
            .ToArrayAsync(cancellationToken);

        return items
            .Select(x => (MessageOutbox)x)
            .ToArray();
    }

    private class PersistedMessageOutbox : MessageOutbox
    {
        public long Id { get; set; }
    }
}
