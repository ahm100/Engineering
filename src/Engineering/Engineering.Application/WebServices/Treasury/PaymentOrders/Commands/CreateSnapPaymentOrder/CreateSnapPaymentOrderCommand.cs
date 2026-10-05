using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.Seasons;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateSnapPaymentOrder;

public record CreateSnapPaymentOrderCommand(
        List<TransportationRequest> SnapRequests,
        List<MetaDataServices.ThirdParties.Models.ThirdParty>? ThirdParties,
        Season Season,
        decimal? ConfirmedPrice,
        DateTime? ConfirmedPaymentDate,
        string? ThirdpartyName,
        string? Description,
        long? DefaultCurrencyId,
        string? PettyCashId,
        bool IsPettyCash,
        long? ThirdpartyId,
        long? BankAccountId,
        long? CostCategoryId,
        long? CostGroupId,
        long? DocumentTypeId,
        long? PreferentialTypeId
    ) : ICommand<CreatePaymentOrderWithAutoDetailResponseModel?>;
