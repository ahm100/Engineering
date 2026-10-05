namespace Engineering.Application.Services.TelegramChats.Models.SendTestTelegramMessage;

public record SendTestTelegramMessageRequest(
    string ChatId,
    string? MessageTest) : IHttpRequest;