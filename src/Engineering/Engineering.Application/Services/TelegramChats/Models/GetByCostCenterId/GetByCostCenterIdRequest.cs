namespace Engineering.Application.Services.TelegramChats.Models.GetByCostCenterId;

public record GetByCostCenterIdRequest(
    long CostCenterId,
    long? ProjectId,
    int PageIndex,
    int PageSize
) : IHttpRequest;