using Engineering.Application.Services.TelegramChats.TelegramServices.Models;

namespace Engineering.Application.Services.TelegramChats.Models.TemporaryDeliveryTelegramMessage;

public record TemporaryDeliveryTelegramMessageRequest(
    long WarehouseId,
    string? WarehouseName,
    string? DestinationWarehouseName,
    string? ReceiverDelivery,
    List<ProductInvoiceForTelegramModel>? Products,
    string? RequestNumber,
    long? CommercialRequestId,
    DateTime? CreateDate,
    DateTime? EnterDate,
    string? Creator,
    string? ConfirmCreator,
    List<string>? DocumentUrls
) : IHttpRequest;

