using Engineering.Domain.Entities.Messages;
using MessageSender.ClientSdk.Messaging;
using System.Text.Json;

namespace Engineering.Persistence.Configurations.Messages;

public class MessageOutboxEntityConfiguration : IEntityTypeConfiguration<MessageOutboxEntity>
{
    public const string TableName = "MessageOutboxEntities";
    public void Configure(EntityTypeBuilder<MessageOutboxEntity> builder)
    {
        builder.MetaConfiguration<MessageOutboxEntity, long>(TableName);

        builder.Property(p => p.Envelope)
            .HasConversion(
                env => JsonSerializer.Serialize(env, JsonSerializerOptions.Default),
                str => JsonSerializer.Deserialize<MessageEnvelope>(str, JsonSerializerOptions.Default)!
            );

        builder.Property(p => p.BrokerAudit)
            .IsPrincipalId();
    }
}
