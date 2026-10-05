namespace Engineering.Application.Services.TelegramChats.Models.CommercialPaymentTelegramMessage;

public record CommercialPaymentTelegramMessageRequest(
    long? ProjectId,
    string? DocumentNumber,
    string? ShabaNo,
    decimal? Amount,
    DateTime? CreateDate,
    string? Reason,
    string? Description,
    string? ThirdPartyName,
    List<string>? DocumentUrls,
    string[]? OrderBy
) : IHttpRequest;