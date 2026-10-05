using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;
namespace Engineering.Infra.Providers.MessageSender;

public class MessageSenderService : IMessageSenderService
{
    private readonly IMessageSenderProvider _MessageSenderProvider;

    public MessageSenderService(IMessageSenderProvider MessageSenderProvider)
    {
        _MessageSenderProvider = MessageSenderProvider;
    }

    public async Task<Result<CreateMessageResponse>> CreateMessage(CreateMessageRequest request, CT ct)
    {

        var result = await _MessageSenderProvider.CreateMessage(request.receiver, request.content, request.priority, request.type, ct);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        return result;
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }

}