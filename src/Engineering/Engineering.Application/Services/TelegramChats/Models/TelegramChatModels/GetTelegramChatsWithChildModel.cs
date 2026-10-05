namespace Engineering.Application.Services.TelegramChats.Models.TelegramChatModels;

public record GetTelegramChatsWithChildModel
{
    public long Id { get; set; }
    public string? ChatName { get; set; } = string.Empty;
    public string ChatUrl { get; set; } = string.Empty;
    public string ChatId { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public long CostCenterId { get; set; }
    public long? ProjectId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; }
}
