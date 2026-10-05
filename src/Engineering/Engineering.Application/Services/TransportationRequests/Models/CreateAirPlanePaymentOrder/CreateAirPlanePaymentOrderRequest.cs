using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.CreateAirPlanePaymentOrder;

public record CreateAirPlanePaymentOrderRequest(
    long AirPlaneRequestId,
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
    long? PreferentialTypeId
     ) : IHttpRequest;
