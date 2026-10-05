namespace Engineering.Application.Services.TelegramChats.Models.TelegramChatModels;

public record GetTelegramChatsModel(
    long Id,
    string TelegramChatName,
    string TelegramChatCode,
    long CategoryId,
    string CategoryName,
    string CategoryCode,
    bool IsActive
    );
