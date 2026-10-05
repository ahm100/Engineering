namespace Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;

public record CreateMessageResponse(
    Guid Id,
    string Receiver,
    string Sender,
    string Content,
    MessagePriority Priority,
    MessageType Type,
    MessageStatus Status);