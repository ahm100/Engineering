
namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;
public record GetPaymentOrdersTotalSummaryRequest(
    List<GetPaymentOrdersTotalSummaryRequestModel> Models
    ) : IHttpRequest;

public record GetPaymentOrdersTotalSummaryRequestModel
{
    public Guid? ThirdPartyPreferentialReferenceCode { get; set; }
};
