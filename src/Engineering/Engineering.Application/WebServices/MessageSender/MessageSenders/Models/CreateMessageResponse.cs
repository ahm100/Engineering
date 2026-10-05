
namespace Engineering.Application.WebServices.MessageSender.MessageSenders.Models;

public record CreateMessageSenderResponse(Guid Id, string Receiver, string Sender, string Content, MessagePriority Priority, MessageType Type, MessageStatus Status);