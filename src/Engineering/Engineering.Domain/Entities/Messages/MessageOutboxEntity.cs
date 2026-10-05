using Gita.Backend.Shared.Domain.Types;
using MessageSender.ClientSdk.Messaging;
using MessageSender.ClientSdk.OutboxPattern;

namespace Engineering.Domain.Entities.Messages;

public class MessageOutboxEntity : AuditableEntity<MessageOutboxEntity>
{
    public MessageOutboxProcessStates Status { get; set; }
    public string? ErrorMessage { get; set; }
    public int ErrorRetries { get; set; } = 0;
    public DateTime? ErrorTime { get; set; }
    public PrincipalId? BrokerAudit { get; set; }
    public DateTime QueuedAt { get; set; }
    public MessageEnvelope Envelope { get; set; }

    public MessageOutboxEntity()
    {

    }
}
