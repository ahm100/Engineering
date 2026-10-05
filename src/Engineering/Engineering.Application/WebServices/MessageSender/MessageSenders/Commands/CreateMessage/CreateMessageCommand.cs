using Engineering.Application.WebServices.MessageSender.MessageSenders.Models;
using Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;

namespace Engineering.Application.WebServices.MessageSender.MessageSenders.Commands.CreateMessage;

public record CreateMessageCommand(
    string receiver,
    string content,
    MessagePriority priority,
    MessageType type) : ICommand<CreateMessageResponse?>;