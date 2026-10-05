namespace Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;

public record CreateMessageRequest(
    string receiver,
    string content,
    int priority,
    int type
    );