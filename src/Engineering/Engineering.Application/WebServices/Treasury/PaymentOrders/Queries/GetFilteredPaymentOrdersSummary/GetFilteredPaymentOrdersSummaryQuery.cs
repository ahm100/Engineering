using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetFilteredPaymentOrdersSummary;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Queries.GetFilteredPaymentOrdersSummary;

public record GetFilteredPaymentOrdersSummaryQuery(
    long MetaThirdPartyId,
    long ProjectId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetFilteredPaymentOrdersSummaryModel>>>;
