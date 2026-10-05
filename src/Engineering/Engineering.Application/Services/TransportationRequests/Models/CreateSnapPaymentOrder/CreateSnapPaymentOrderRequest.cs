namespace Engineering.Application.Services.TransportationRequests.Models.CreateSnapPaymentOrder;

public record CreateSnapPaymentOrderRequest(
    List<long> SnapRequestIds,
    decimal? ConfirmedPrice,
    string? ThirdPartyName,
    string? Description,
    DateTime? PaymentDate,
    long? SeasonId,
    string? PettyCashId,
    bool IsPettyCash,
    long? ThirdpartyId,
    long? BankAccountId,
    string? IBAN,
    long? CostCategoryId,
    long? CostGroupId,
    long? DocumentTypeId,
    long? PreferentialTypeId
     ) : IHttpRequest;
