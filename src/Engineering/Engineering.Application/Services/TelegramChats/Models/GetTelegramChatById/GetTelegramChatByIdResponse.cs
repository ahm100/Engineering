using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramChats.Models.GetTelegramChatById;

public record GetTelegramChatByIdResponse
{
    public long Id { get; set; }
    public string? ChatName { get; private set; } = string.Empty;
    public string ChatUrl { get; private set; } = string.Empty;
    public string ChatId { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public long CostCenterId { get; private set; }
    public long? ProjectId { get; private set; }
    public string CostCenterName { get; private set; } = string.Empty;
    public string? ProjectName { get; private set; }
    public List<GetTelegramChatTypeByIdResponse>? TelegramChatTypes { get; set; } = new();

}

public record GetTelegramChatTypeByIdResponse
{
    public string? ChatName { get; private set; } = string.Empty;
    public string ChatUrl { get; private set; } = string.Empty;
    public string ChatId { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
    public TelegramMessageType TelegramMessageType { get; set; }
    public string? TelegramMessageTypeDescription => TelegramMessageType.GetEnumDescription();
}