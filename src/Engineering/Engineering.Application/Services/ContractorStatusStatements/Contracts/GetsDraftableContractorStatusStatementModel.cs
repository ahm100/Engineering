using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Contracts;

public class GetsDraftableContractorStatusStatementModel
{
    public long Id { get; set; }
    public long? CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public long? BranchId { get; set; }
    public string? BranchName { get; set; } = string.Empty;
    public long? SeasonId { get; set; }
    public string? SeasonName { get; set; } = string.Empty;
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
    public CSSType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
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
    public bool PrimaryManagerConfirmed { get; set; }
    public decimal? FinalManagerConfirmedAmount { get; set; }
    public bool FinalManagerConfirmed { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? ProjectManagmentDescription { get; set; } = string.Empty;
    public string? ManagmentDescription { get; set; } = string.Empty;
    public string? PrimaryManagerDescription { get; set; } = string.Empty;
    public string? FinalManagerDescription { get; set; } = string.Empty;
    public string? LastDescription { get; set; } = string.Empty;
    public bool IsLast { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
}
