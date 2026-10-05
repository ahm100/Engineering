using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSS.Service;

public record GetsIntegratedCSSResponse(
    List<GetsIntegratedCSSResponseModel> Data,
    int RowCount
    );

public record GetsIntegratedCSSResponseModel
{
    public string Id => $"{ContractorId}-{ContractorId}";
    public long? ContractorId { get; set; }
    public Guid? ContractorReferenceCode { get; set; }
    public string? Contractor { get; set; }
    public decimal? PrimaryManagerConfirmedAmount { get; set; }
    public decimal? FinalManagerConfirmedAmount { get; set; }
    public decimal? ManagementApprovalAmount { get; set; }
    public decimal? ManagementConfirmedAmount { get; set; }
    public decimal? ConfirmedPrice { get; set; }
    public decimal? PaymentedAmount { get; set; }
    public decimal? Amount { get; set; }
    public decimal? FilledAmount { get; set; }
    public decimal? RefundAmount { get; set; }
    public decimal? RemainigAmount { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public string? LastDescription { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public List<GetsIntegratedCSSModel> ContractorStatusStatements { get; set; } = new();
}

public record GetsIntegratedCSSReferenceModel
{
    public Guid? CostCenterReferenceCode { get; set; }
    public Guid? ContractorReferenceCode { get; set; }
}

public record GetsIntegratedCSSModel
{
    public long Id { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public Guid? CostCenterReferenceCode { get; set; }
    public long? ProjectId { get; set; }
    public string? Project { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public Guid? ContractorReferenceCode { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public decimal? CreatorConfirmedAmount { get; set; }
    public decimal? ProjectManagerConfirmedAmount { get; set; }
    public decimal? ManagementConfirmedAmount { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? Created { get; set; }
    public string? StartDateShamsi { get; set; } = string.Empty;
    public string? EndDateShamsi { get; set; } = string.Empty;
    public CSSStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public decimal? FinalTotalAmount { get; set; }
    public decimal? TotalPercentageDoingJobWell { get; set; }
    public decimal? TotalDoingJobWellAmount { get; set; }
    public decimal? TotalAdvancePaymentAmount { get; set; }
    public decimal? TotalPercentageAdvancePayment { get; set; }
    public decimal? TotalDailyLatenessPenalty { get; set; }
    public decimal? TotalWorkDonePercent { get; set; }
    public decimal? TotalWorkDeliveryPercent { get; set; }
    public decimal? TotalWorkCompletionPercent { get; set; }
    public decimal? ProductsAmount { get; set; }
    public decimal? FinesAmount { get; set; }
    public decimal? UserApprovalAmount { get; set; }
    public decimal? ProjectManagerApprovalAmount { get; set; }
    public decimal? ManagementApprovalAmount { get; set; }
    public decimal? DiscountedAmount { get; set; }
    public decimal? RewardsAmount { get; set; }
    public decimal? ThirdPartiesAmount { get; set; }
    public decimal? ConfirmedPrice { get; set; }
    public decimal? PaymentedAmount { get; set; }
    public decimal? PrimaryManagerConfirmedAmount { get; set; }
    public bool? PrimaryManagerConfirmed { get; set; }
    public decimal? FinalManagerConfirmedAmount { get; set; }
    public bool? FinalManagerConfirmed { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? ProjectManagmentDescription { get; set; } = string.Empty;
    public string? ManagmentDescription { get; set; } = string.Empty;
    public string? PrimaryManagerDescription { get; set; } = string.Empty;
    public string? FinalManagerDescription { get; set; } = string.Empty;
    public string? LastDescription { get; set; } = string.Empty;
    public bool IsLast { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}