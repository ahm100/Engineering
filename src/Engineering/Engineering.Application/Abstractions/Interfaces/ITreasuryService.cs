using Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.DeletePaymentOrder;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetFilteredPaymentOrdersSummary;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;

namespace Engineering.Application.Abstractions.Interfaces;

public interface ITreasuryService
{
    Task<Result<CreatePaymentOrderWithAutoDetailResponse>> CreatePaymentOrderWithAutoDetail(
        CreatePaymentOrderWithAutoDetailRequest request, CT ct);

    Task<Result<GetPaymentOrdersTotalSummaryResponse>> GetPaymentOrdersTotalSummary(
        GetPaymentOrdersTotalSummaryRequest request, CT ct);

    Task<Result<GetFilteredPaymentOrdersSummaryResponse>> GetFilteredPaymentOrdersSummary(
        GetFilteredPaymentOrdersSummaryRequest request, CT ct);

    Task<Result<DeletePaymentOrderResponse>> DeletePaymentOrder(
        DeletePaymentOrderRequest request, CT ct);

}