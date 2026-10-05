using Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;

namespace Engineering.Application.Abstractions.Interfaces;

public interface IMessageSenderService
{
    Task<Result<CreateMessageResponse>> CreateMessage(CreateMessageRequest request, CT ct);
}