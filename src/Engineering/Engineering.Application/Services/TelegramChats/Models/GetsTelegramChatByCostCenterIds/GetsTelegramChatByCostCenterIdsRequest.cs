
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramChats.Models.GetsTelegramChatByCostCenterIds;

public record GetsTelegramChatByCostCenterIdsRequest(
    List<long> CostCenterIds,
    TelegramMessageType? TelegramMessageType,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
) : IHttpRequest;