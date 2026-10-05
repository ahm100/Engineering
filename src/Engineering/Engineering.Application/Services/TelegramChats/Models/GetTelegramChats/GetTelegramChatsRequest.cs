using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramChats.Models.GetTelegramChats;

public record GetTelegramChatsRequest(
    List<long>? Ids,
    TelegramMessageType? TelegramMessageType,
    string? FilterData,
    long? CostCenterId,
    long? ProjectId,
    string? ChatName,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
) : IHttpRequest;