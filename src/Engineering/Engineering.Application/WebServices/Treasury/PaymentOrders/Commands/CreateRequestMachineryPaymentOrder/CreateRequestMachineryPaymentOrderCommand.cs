using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.CreateRequestMachineryPaymentOrder;

public record CreateRequestMachineryPaymentOrderCommand(
    RequestMachineryStatusStatement RequestMachineryStatusStatement,
    ThirdPartyByIdModel? ThirdParty,
    Season Season,
    decimal? ConfirmedPrice,
    long? ConfirmedBankAccountId,
    DateTime? ConfirmedPaymentDate,
    string? Description,
    string? pettyCashId,
    bool IsPettyCash,
    long? CostCategoryId,
    long? CostGroupId,
    long? DocumentTypeId,
    long? PreferentialTypeId) : ICommand<CreatePaymentOrderWithAutoDetailResponseModel?>;
