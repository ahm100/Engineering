using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.DeletePaymentOrder;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.CreatePaymentOrderWithAutoDetail;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetFilteredPaymentOrdersSummary;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;

namespace Engineering.Infra.Providers.Treasury;

public class TreasuryService : ITreasuryService
{
    private readonly ITreasuryProvider _treasuryProvider;

    public TreasuryService(ITreasuryProvider TreasuryProvider)
    {
        _treasuryProvider = TreasuryProvider;
    }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.

    public async Task<Result<CreatePaymentOrderWithAutoDetailResponse?>> CreatePaymentOrderWithAutoDetail(
        CreatePaymentOrderWithAutoDetailRequest request, CT ct)
    {
        var result = await _treasuryProvider.CreatePaymentOrderWithAutoDetail(request, ct);
        return result;
    }
    public async Task<Result<GetPaymentOrdersTotalSummaryResponse?>> GetPaymentOrdersTotalSummary(
        GetPaymentOrdersTotalSummaryRequest request, CT ct)
    {
        var result = await _treasuryProvider.GetPaymentOrdersTotalSummary(request, ct);
        return result;
    }

    public async Task<Result<GetFilteredPaymentOrdersSummaryResponse?>> GetFilteredPaymentOrdersSummary(
        GetFilteredPaymentOrdersSummaryRequest request, CT ct)
    {
        var result = await _treasuryProvider.GetFilteredPaymentOrdersSummary(request, ct);
        return result;
    }

    public async Task<Result<DeletePaymentOrderResponse?>> DeletePaymentOrder(
        DeletePaymentOrderRequest request, CT ct)
    {
        return await _treasuryProvider.DeletePaymentOrder(request.Id, ct);
    }

#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
}