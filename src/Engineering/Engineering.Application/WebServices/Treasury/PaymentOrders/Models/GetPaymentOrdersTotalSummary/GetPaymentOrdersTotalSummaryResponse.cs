
namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;

public class GetPaymentOrdersTotalSummaryResponse
{
    [JsonProperty("data")]
    public List<GetPaymentOrdersTotalSummaryModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}

public record GetPaymentOrdersTotalSummaryModel
{
    public Guid ThirdPartyPreferentialReferenceCode { get; set; }
    public Guid? CostCenterPreferentialReferenceCode { get; set; }
    public decimal Amount { get; set; }
    public decimal FilledAmount { get; set; }
    public decimal RefundAmount { get; set; }
    public decimal RemainigAmount { get; set; }
}

