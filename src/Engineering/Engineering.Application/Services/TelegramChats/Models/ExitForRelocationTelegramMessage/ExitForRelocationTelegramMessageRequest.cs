using Engineering.Application.Services.TelegramChats.TelegramServices.Models;

namespace Engineering.Application.Services.TelegramChats.Models.ExitForRelocationTelegramMessage;


public record ExitForRelocationTelegramMessageRequest(
    long WarehouseId,
    long? DestinationWarehouseId,
    string? SourceWarehouseName,
    string? DestinationWarehouseName,
    string? DocumentNumber,
    List<ProductInvoiceForTelegramModel>? Products,
    string? RequestNumber,
    long? CommercialRequestId,
    DateTime? CreateDate,
    DateTime? ExitDate,
    string? Creator,
    string? ConfirmCreator,
    List<string>? DocumentUrls
) : IHttpRequest;