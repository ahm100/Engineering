namespace Engineering.Application.Services.TelegramMessageHistorys.Models.ExecuteSendTelegramMessage;

public record ExecuteSendTelegramMessageRequest(List<long> Ids) : IHttpRequest;