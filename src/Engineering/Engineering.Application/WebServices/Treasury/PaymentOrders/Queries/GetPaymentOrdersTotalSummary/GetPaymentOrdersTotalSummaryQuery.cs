using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Queries.GetPaymentOrdersTotalSummary;

public record GetPaymentOrdersTotalSummaryQuery(
    List<GetPaymentOrdersTotalSummaryRequestModel> Models
    ) : IQuery<DataResult<List<GetPaymentOrdersTotalSummaryModel>>>;
