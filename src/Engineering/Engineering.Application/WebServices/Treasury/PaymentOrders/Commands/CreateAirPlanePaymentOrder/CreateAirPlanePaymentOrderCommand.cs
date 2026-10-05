using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.Seasons;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateAirPlanePaymentOrder;

public record CreateAirPlanePaymentOrderCommand(
    TransportationRequest AirPlane,
    ThirdPartyByIdModel? ThirdParty,
    Season Season,
    decimal? ConfirmedPrice,
    long? ConfirmedBankAccountId,
    DateTime? ConfirmedPaymentDate,
    string? Description,
    string? PettyCashId,
    bool IsPettyCash,
    long? CostCategoryId,
    long? CostGroupId,
    long? DocumentTypeId,
    long? PreferentialTypeId) : ICommand<CreatePaymentOrderWithAutoDetailResponseModel?>;
