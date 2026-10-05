using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSPayments;
public record GetCSSPaymentsResponse(
    List<GetCSSPaymentsModel> Data,
    int RowCount
);

public record GetCSSPaymentsModel
{
    public long Id { get; set; }
    public decimal CalculatedAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public decimal UserAmount { get; set; }
    public string? UserDescription { get; set; }
    public CSSPaymentStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public decimal? ProjectManagerAmount { get; set; }
    public string? ProjectManagerDescription { get; set; }
    public decimal? ManagementAmount { get; set; }
    public string? ManagementDescription { get; set; }
    public decimal? PrimaryManagerAmount { get; set; }
    public string? PrimaryManagerDescription { get; set; }
    public decimal? FinalManagerAmount { get; set; }
    public string? FinalManagerDescription { get; set; }
    public decimal? PaymentAmount { get; set; }
    public string? PaymentDescription { get; set; }
    public long? PaymentOrderId { get; set; }
    public DateTime? PaymentDate { get; set; }
    public decimal? TreasuryPaid { get; set; }
    public DateTime Created { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}