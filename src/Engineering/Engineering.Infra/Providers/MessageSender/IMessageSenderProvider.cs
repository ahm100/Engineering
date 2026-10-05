using Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;

namespace Engineering.Infra.Providers.MessageSender;

public interface IMessageSenderProvider
{
    [Multipart]
    [Post("/message/send")]
    Task<Result<CreateMessageResponse?>> CreateMessage(
        [AliasAs("receiver")] string receiver, [AliasAs("content")] string content, [AliasAs("priority")] int priority, [AliasAs("type")] int type, CT ct);

}