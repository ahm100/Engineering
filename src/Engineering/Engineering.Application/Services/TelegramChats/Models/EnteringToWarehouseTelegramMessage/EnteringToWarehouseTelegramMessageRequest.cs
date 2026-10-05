using Engineering.Application.Services.TelegramChats.TelegramServices.Models;

namespace Engineering.Application.Services.TelegramChats.Models.EnteringToWarehouseTelegramMessage;

public record EnteringToWarehouseTelegramMessageRequest(
    long WarehouseId,
    string? WarehouseName,
    List<ProductInvoiceForTelegramModel>? Products,
    string? RequestNumber,
    string? ReceiverDelivery,
    long? CommercialRequestId,
    DateTime? CreateDate,
    DateTime? EnterDate,
    string? Creator,
    string? ConfirmCreator,
    List<string>? DocumentUrls
) : IHttpRequest;