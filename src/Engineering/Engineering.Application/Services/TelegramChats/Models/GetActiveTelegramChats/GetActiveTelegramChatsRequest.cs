namespace Engineering.Application.Services.TelegramChats.Models.GetActiveTelegramChats;

public record GetActiveTelegramChatsRequest(
    string? FilterData,
    long? CostCenterId,
    long? ProjectId,
    string? Name,
    int PageIndex,
    int PageSize
) : IHttpRequest;