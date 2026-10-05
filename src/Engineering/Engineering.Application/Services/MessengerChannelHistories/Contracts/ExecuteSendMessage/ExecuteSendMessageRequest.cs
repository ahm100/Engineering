namespace Engineering.Application.Services.TelegramMessageHistorys.Models.ExecuteSendMessage;

public record ExecuteSendMessageRequest(List<long> Ids) : IHttpRequest;