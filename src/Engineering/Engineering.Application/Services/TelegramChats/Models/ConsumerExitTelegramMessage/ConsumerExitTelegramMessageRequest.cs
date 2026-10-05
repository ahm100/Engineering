using Engineering.Application.Services.TelegramChats.TelegramServices.Models;

namespace Engineering.Application.Services.TelegramChats.Models.ConsumerExitTelegramMessage;

public record ConsumerExitTelegramMessageRequest(
    long WarehouseId,
    string? WarehouseName,
    string? DocumentNumber,
    List<ProductInvoiceForTelegramModel>? Products,
    string? RequestNumber,
    string? ThirdPartyName,
    string? ReceiverDelivery,
    long? CommercialRequestId,
    DateTime? CreateDate,
    DateTime? ExitDate,
    string? Creator,
    string? ConfirmCreator,
    List<string>? DocumentUrls
    ) : IHttpRequest;