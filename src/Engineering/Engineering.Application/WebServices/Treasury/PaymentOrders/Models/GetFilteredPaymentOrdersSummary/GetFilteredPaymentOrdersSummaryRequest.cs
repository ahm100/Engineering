
namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;

public record GetFilteredPaymentOrdersSummaryRequest : IHttpRequest
{
    public long MetaThirdPartyId { get; set; }
    public long ProjectId { get; set; }
    public int Type { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
};
