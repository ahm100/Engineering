using Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Queries.GetsDraftableContractorCostOver;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDraftCreator;

public record ContractorStatusStatementDraftCreatorResponse
{
    public long ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public long CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? CostCenterCode { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public long? ProjectManagerId { get; set; }
    public string? ProjectManager { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<GetsDraftableContractorContractHeaderModel> ContractorContractHeaders { get; set; } = new();

    public decimal? FixContractorContractAmounts { get; set; } = 0;

    public decimal? TotalPercentageAdvancePayment =>
    FinalTotalAmount > 0
        ? TotalAdvancePaymentAmount * 100 / FinalTotalAmount
        : 0;

    public decimal? TotalDailyLatenessPenalty =>
        ContractorContractHeaders.Any()
            ? ContractorContractHeaders.Sum(x => x.TotalDailyLatenessPenalty) / ContractorContractHeaders.Count
            : 0;

    public decimal? FinalTotalAmount =>
        ContractorContractHeaders.Sum(x => x.FinalTotalAmount) ?? 0;

    public decimal? TotalPercentageDoingJobWell =>
        ContractorContractHeaders.Sum(x => x.TotalPercentageDoingJobWell) ?? 0;

    public decimal? TotalDoingJobWellAmount =>
        ContractorContractHeaders.Sum(x => x.TotalDoingJobWellAmount) ?? 0;

    public decimal? TotalAdvancePaymentAmount =>
        ContractorContractHeaders.Sum(x => x.TotalAdvancePaymentAmount) ?? 0;

    public decimal? WorkedContractorContractPrice { get; set; } = 0;

    public decimal? DiscountAmount => Discount.TotalPrice;

    public decimal? PendToPayment { get; set; } = 0;

    public decimal? AllCost =>
        Fine.TotalPrice + (DiscountAmount ?? 0);

    public decimal? AllNonCost =>
        (WorkedContractorContractPrice ?? 0)
        + (FixContractorContractAmounts ?? 0)
        + CostOver.TotalPrice
        + Product.TotalPrice
        + Reward.TotalPrice;

    public decimal? PayableAmount =>
        AllNonCost - AllCost;

    public decimal? CanPayableAmount =>
        (FinalTotalAmount ?? 0)
        + CostOver.TotalPrice
        + Product.TotalPrice
        + Reward.TotalPrice
        - AllCost
        - (Paymented ?? 0);

    public bool? HaveConfirmed { get; set; } = false;
    public ContractorContractProductDraftModel Product { get; set; } = new();
    public ContractorContractRewardDraftModel Reward { get; set; } = new();
    public ContractorContractFineDraftModel Fine { get; set; } = new();
    public ContractorContractDiscountDraftModel Discount { get; set; } = new();
    public ContractorCostOverDraftModel CostOver { get; set; } = new();
    public List<PaymentOrdersSummaryModel>? PaymentOrdersSummaries { get; set; } = new();

    public decimal? TotalCSSPrice => AllNonCost - AllCost;
    public decimal? Paymented { get; set; } = 0;
    public decimal? CanPay => (TotalCSSPrice ?? 0) - (Paymented ?? 0);
    public decimal? PaidContractorStatus { get; set; } = 0;
    public List<PaidContractorStatusStatementModel>? PaidContractorStatusStatements { get; set; } = new();
}

public record ContractorCostOverDraftModel
{
    public int TotalCount => CostOvers.Count;
    public decimal TotalPrice => CostOvers.Sum(oo => oo.Amount) ?? 0;
    public List<GetsDraftableContractorCostOverModel> CostOvers { get; set; } = new();
}

public record ContractorContractFineDraftModel
{
    public long TotalCount => Fines.Count;
    public decimal TotalPrice => Fines.Sum(oo => oo.Price);
    public List<ContractorContractFinesDraftModel> Fines { get; set; } = new();
}

public record ContractorContractFinesDraftModel
{
    public long RequestRewardId { get; set; }
    public string? Created { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ManagerDescription { get; set; } = string.Empty;
    public decimal? OfferedPrice { get; set; }
    public decimal Price { get; set; }
    public RequestRewardType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public RequestRewardStatus Status => RequestRewardStatus.Confirmed;
    public string StatusDescription => Status.GetEnumDescription();
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}

public record ContractorContractRewardDraftModel
{
    public long TotalCount => Rewards.Count;
    public decimal TotalPrice => Rewards.Sum(oo => oo.Price);
    public List<ContractorContractRewardsDraftModel> Rewards { get; set; } = new();
}

public record ContractorContractRewardsDraftModel
{
    public long RequestRewardId { get; set; }
    public string? Created { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ManagerDescription { get; set; } = string.Empty;
    public decimal? OfferedPrice { get; set; }
    public decimal Price { get; set; }
    public RequestRewardType Type => RequestRewardType.Reward;
    public string TypeDescription => Type.GetEnumDescription();
    public RequestRewardStatus Status => RequestRewardStatus.Confirmed;
    public string StatusDescription => Status.GetEnumDescription();
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}

public record ContractorContractProductDraftModel
{
    public int TotalCount => Products.Count;
    public decimal TotalPrice => Products.Sum(oo => oo.TotalPrice) ?? 0;
    public List<ContractorContractProductsDraftModel> Products { get; set; } = new();
}

public record ContractorContractProductsDraftModel
{
    public long? RequestGoodsSupplyDetailId { get; set; }
    public decimal? RequestNumber { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? MeasureUnit { get; set; } = string.Empty;
    public decimal? SupplyCount { get; set; }
    public long? ProductGroupId { get; set; }
    public decimal? Price => SupplyCount * UnitPrice;
    public decimal? OtherPrice { get; set; } = 0;
    public decimal? TransferPrice { get; set; } = 0;
    public decimal? PackingPrice { get; set; } = 0;
    public decimal? DiscountByNum { get; set; }
    public decimal? RequestCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TaxNumber { get; set; }
    public decimal? TotalPrice => ((Price + OtherPrice + TransferPrice + TaxNumber + PackingPrice) - DiscountByNum);
    public string? CustomerInvoiceNumber { get; set; } = string.Empty;
    public string? Created { get; set; } = string.Empty;
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}

public record PaymentOrdersSummaryModel
{
    public long Id { get; set; }
    public string? Number { get; set; } = default!;
    public string CurrencyTypeDescription { get; set; } = string.Empty;
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

public record ContractorContractDiscountDraftModel
{
    public long TotalCount => Discounts.Count;
    public decimal TotalPrice => Discounts.Count > 0 ? Discounts.Sum(oo => oo.Price) : 0;
    public List<ContractorContractDiscountsDraftModel> Discounts { get; set; } = new();
}

public record ContractorContractDiscountsDraftModel
{
    public long RequestRewardId { get; set; }
    public string? Created { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ManagerDescription { get; set; } = string.Empty;
    public decimal? OfferedPrice { get; set; }
    public decimal Price { get; set; }
    public RequestRewardType Type => RequestRewardType.Discount;
    public string TypeDescription => Type.GetEnumDescription();
    public RequestRewardStatus Status => RequestRewardStatus.Confirmed;
    public string StatusDescription => Status.GetEnumDescription();
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}