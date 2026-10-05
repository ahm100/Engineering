
namespace Engineering.Application.Services.TelegramChats.Models.ContractorStatementPaymentMessage;

public record ContractorStatementPaymentMessageRequest(
    long? CostCenterId,
    DateTime? Date,
    string? Number,
    string? PaymentOrderNumber,
    string? ThirdParty,
    string? Description,
    string? DefaultDescription,
    string? ShabaNo,
    decimal? Amount,
    List<string>? DocumentUrls
) : IHttpRequest;

