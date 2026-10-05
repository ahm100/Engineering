using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.CreateTransportationRequestPaymentOrder;

public record CreateTransportationRequestPaymentOrderRequest(
    long TransportationRequestId,
    decimal? ConfirmedPrice,
    string? Description,
    DateTime? PaymentDate,
    long? ThirdpartyId,
    long? BankAccountId,
    string? IBAN,
    long? SeasonId,
    string? PettyCashId,
    TransportationPaymentType PaymentType,
    long? CostCategoryId,
    long? CostGroupId,
    long? DocumentTypeId,
    long? PreferentialTypeId,
    bool? IsAirPlane
     ) : IHttpRequest;
