namespace Engineering.Application.Services.TelegramChats.Models.UserChangedTelegramMessage;

public record UserChangedTelegramMessageRequest(
    long UserId,
    string Mobile,
    string Description,
    DateTime Created,
    string? Creator) : IHttpRequest;
