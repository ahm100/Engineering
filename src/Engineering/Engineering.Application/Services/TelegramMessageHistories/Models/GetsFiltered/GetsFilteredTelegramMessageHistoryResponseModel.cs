using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramMessageHistorys.Models.GetsFiltered;

public record GetsFilteredTelegramMessageHistoryResponseModel
{
    public long Id { get; set; }
    public long TelegramChatId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
    public string? FileUrls { get; set; }
    public bool IsSend { get; set; }
    public TelegramMessageType TelegramMessageType { get; set; }
}