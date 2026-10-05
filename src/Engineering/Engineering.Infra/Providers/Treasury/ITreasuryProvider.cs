using Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.DeletePaymentOrder;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetFilteredPaymentOrdersSummary;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;

namespace Engineering.Infra.Providers.Treasury;

public interface ITreasuryProvider
{
    // ایجاد دستور پرداخت
    [Post("/payment-order/create-subsystem-with-auto-detail")]
    Task<Result<CreatePaymentOrderWithAutoDetailResponse?>> CreatePaymentOrderWithAutoDetail(
        [Body] CreatePaymentOrderWithAutoDetailRequest request, CT ct);

    // ایجاد دستور پرداخت
    [Post("/payment-order/getPaymentOrdersTotalSummary")]
    Task<Result<GetPaymentOrdersTotalSummaryResponse?>> GetPaymentOrdersTotalSummary(
        [Body] GetPaymentOrdersTotalSummaryRequest request, CT ct);

    // ایجاد دستور پرداخت
    [Post("/payment-order/getFilteredSummary")]
    Task<Result<GetFilteredPaymentOrdersSummaryResponse?>> GetFilteredPaymentOrdersSummary(
        [Body] GetFilteredPaymentOrdersSummaryRequest request, CT ct);

    // حذف دستور پرداخت
    [Delete("/payment-order/remove")]
    Task<Result<DeletePaymentOrderResponse?>> DeletePaymentOrder(
        [AliasAs("Id")] long Id, CT ct);

}