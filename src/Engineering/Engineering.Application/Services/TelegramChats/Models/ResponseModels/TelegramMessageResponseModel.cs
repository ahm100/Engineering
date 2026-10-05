using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramChats.Models.ResponseModels;

public record TelegramMessageResponseModel
{
    public long? Id { get; set; }
    public string? ChatId { get; set; }
    public DateTime? Created { get; set; }
    public bool? IsActive { get; set; }
    public List<TelegramChatTypeResponseModel>? TelegramChatTypes { get; set; }
}

public record TelegramChatTypeResponseModel
{
    public string? ChatId { get; set; }
    public TelegramMessageType? TelegramMessageType { get; set; }
}