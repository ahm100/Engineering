namespace Engineering.Application.Services.TelegramChats.Models.CommerceRequestWarehouseStatusChangeTelegramMessage;

public record CommerceRequestWarehouseStatusChangeTelegramMessageRequest(
    long CostCenterId,
    string? Status,
    long? RequestNumber,
    string? CostCenterName,
    string? ProjectName,
    string? Description,
    DateTime CreateDate,
    long CreatorChangeId,
    long CreatorRequestId,
    string? ProductName,
    string? ProductCode,
    decimal? RequestedCount,
    string? CommerceRequestType,
    string? CommercialRequestNumber,
    long? CommercialRequestId
) : IHttpRequest;