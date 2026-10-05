
namespace Engineering.Application.Services.TelegramChats.Models.AcceptPaymentOrderTelegramMessage;

public record AcceptPaymentOrderTelegramMessageRequest(
    long? CostCenterId,
    string? ReferenceDetails,
    DateTime? CreateDate,
    string? Creator,
    List<string>? DocumentUrls
) : IHttpRequest;

