

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetFilteredPaymentOrdersSummary;

public class GetFilteredPaymentOrdersSummaryResponse
{
    [JsonProperty("data")]
    public List<GetFilteredPaymentOrdersSummaryModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}

public record GetFilteredPaymentOrdersSummaryModel
{
    public long Id { get; set; }
    public string? Number { get; set; } = default!;
    public int CurrencyType { get; set; }
    public string CurrencyTypeDescription { get; set; } = string.Empty;
    public int Status { get; set; }
    public string StatusDescription { get; set; } = string.Empty;
    public long ThirdPartyId { get; set; }
    public string ThirdPartyName { get; set; } = default!;
    public long PaymentOrderTypeId { get; set; }
    public string PaymentOrderTypeTitle { get; set; } = default!;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long CreatorId { get; set; }
    public string CreatorName { get; set; } = default!;
    public DateTime IssueDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal PackagingCost { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Discount { get; set; }
    public decimal ValueAddedTax { get; set; }
    public decimal Amount { get; set; }
    public decimal FilledAmount { get; set; }
    public decimal RefundAmount { get; set; }

    public decimal RemainigAmount { get; set; }
    public bool IsManually { get; set; }
    public bool? IsShebaManually { get; set; }
    public DateTime? Created { get; set; }
    public string? Description { get; set; }

    public long? CostCategoryId { get; set; }
    public string? CostCategoryCode { get; set; }
    public string? CostCategoryTitle { get; set; }
    public long? CostGroupId { get; set; }
    public string? CostGroupCode { get; set; }
    public string? CostGroupTitle { get; set; }
    public string? ReferenceNo { get; set; }

}
