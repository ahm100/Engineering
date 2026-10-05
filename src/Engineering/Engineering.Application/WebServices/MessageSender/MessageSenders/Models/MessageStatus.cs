namespace Engineering.Application.WebServices.MessageSender.MessageSenders.Models;

public enum MessageStatus
{
    New = 0,
    Sent = 1,
    Error = 2,
    NotSeen = 1,
    Seen = 3
}