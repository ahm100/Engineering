using Engineering.Application.Services.ContractorStatusStatements.Models.ContractorStatusStatementDraftCreator;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;

public record GetContractorStatusStatementByIdResponse
{
    public long Id { get; set; }
    public bool MultiPayment { get; set; }
    public decimal? FinalTotalAmount { get; set; }
    public decimal? TotalCSSPrice => ProductsPrice + RewardsPrice + FixedAmount + ServiceAmount + CostOversPrice - (DiscountsPrice + FinesPrice + ForContractorProductsPrice);
    public decimal? Paymented { get; set; } = 0;
    public decimal? CanPay => (TotalCSSPrice ?? 0) - (Paymented ?? 0) -
        (FinalManagerConfirmedAmount is not 0 ? FinalManagerConfirmedAmount :
        PrimaryManagerConfirmedAmount is not 0 ? PrimaryManagerConfirmedAmount :
        ManagementConfirmedAmount is not 0 ? ManagementConfirmedAmount :
        ProjectManagerConfirmedAmount is not 0 ? ProjectManagerConfirmedAmount :
        CreatorConfirmedAmount);
    public long? CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public long? BranchId { get; set; }
    public string? BranchName { get; set; } = string.Empty;
    public long? SeasonId { get; set; }
    public string? SeasonName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? Project { get; set; } = string.Empty;
    public Guid? ProjectPreferentialCode { get; set; }
    public decimal? FixedAmount { get; set; }
    public decimal? ServiceAmount { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public Guid? ContractorPreferentialCode { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; } = string.Empty;
    public string? Code { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? StartDateShamsi => StartDate is null ? null : TimeCalculator.ConvertToShamsi(StartDate);
    public string? EndDateShamsi => EndDate is null ? null : TimeCalculator.ConvertToShamsi(EndDate);
    public CSSStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public CSSType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public decimal? PayableAmount { get; set; }
    public decimal? RemainingAmount { get; set; }
    public decimal? CanPayableAmount { get; set; }
    public decimal? DiscountPrice { get; set; }
    public decimal? TotalPercentageDoingJobWell { get; set; }
    public decimal? TotalDoingJobWellAmount { get; set; }
    public decimal? TotalAdvancePaymentAmount { get; set; }
    public decimal? TotalPercentageAdvancePayment { get; set; }
    public decimal? CreatorConfirmedAmount { get; set; }
    public decimal? ProjectManagerConfirmedAmount { get; set; }
    public decimal? ManagementConfirmedAmount { get; set; }
    public decimal? TotalDailyLatenessPenalty { get; set; }
    public decimal? TotalWorkDonePercent { get; set; }
    public decimal? TotalWorkDeliveryPercent { get; set; }
    public decimal? TotalWorkCompletionPercent { get; set; }
    public decimal? ProductsAmount { get; set; }
    public decimal? FinesAmount { get; set; }
    public decimal? RewardsAmount { get; set; }
    public decimal? CostOversAmount { get; set; }
    public decimal? ThirdPartiesAmount { get; set; }
    public decimal? DiscountedAmount { get; set; } = 0;
    public decimal? PendToPayment { get; set; } = 0;
    public decimal? DiscountPayment => Paymented - DiscountedAmount;
    public decimal? PaidAmount { get; set; }
    public decimal? PaidContractorStatus { get; set; }
    public decimal? PrimaryManagerConfirmedAmount { get; set; }
    public bool? PrimaryManagerConfirmed { get; set; }
    public decimal? FinalManagerConfirmedAmount { get; set; }
    public bool? FinalManagerConfirmed { get; set; }
    public bool? IsLast { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? ProjectManagmentDescription { get; set; } = string.Empty;
    public string? ManagmentDescription { get; set; } = string.Empty;
    public string? PrimaryManagerDescription { get; set; } = string.Empty;
    public string? FinalManagerDescription { get; set; } = string.Empty;
    public string? LastDescription { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public decimal ProductsPrice => Products is not null ? Products.Sum(x => x.TotalPrice ?? 0) : 0;
    public decimal ForContractorProductsPrice => ForContractorProducts is not null ? ForContractorProducts.Sum(x => x.TotalPrice ?? 0) : 0;
    public decimal DiscountsPrice => Discounts is not null ? Discounts.Sum(x => x.Price) : 0;
    public decimal FinesPrice => Fines is not null ? Fines.Sum(x => x.OfferedPrice) : 0;
    public decimal RewardsPrice => Rewards is not null ? Rewards.Sum(x => x.OfferedPrice) : 0;
    public decimal CostOversPrice => Products is not null ? CostOvers.Sum(x => x.Amount ?? 0) : 0;
    public decimal ContractorContractHeadersPrice => ContractorContractHeaders is not null ? ContractorContractHeaders.Sum(x => x.FinalTotalAmount ?? 0) : 0;
    public List<GetModeledContractorStatusStatementByIdProduct>? Products { get; set; } = new();
    public List<GetModeledContractorStatusStatementByIdForContractorProduct>? ForContractorProducts { get; set; } = new();
    public List<GetContractorStatusStatementByIdDiscounts>? Discounts { get; set; } = new();
    public List<GetModeledContractorStatusStatementByIdFines>? Fines { get; set; } = new();
    public List<GetModeledContractorStatusStatementByIdRewards>? Rewards { get; set; } = new();
    public List<GetModeledContractorStatusStatementByIdCostOvers>? CostOvers { get; set; } = new();
    public List<GetsCSSContractorContractHeaderModel>? ContractorContractHeaders { get; set; } = new();
    public List<PaidContractorStatusStatementModel>? PaidContractorStatusStatements { get; set; } = new();
    public List<PaymentOrdersSummaryModel>? PaymentOrdersSummaries { get; set; } = new();
}

public record GetModeledContractorStatusStatementByIdCostOvers
{
    public long Id { get; set; }
    public long CostOverDetailId { get; set; }
    public long CostOverId { get; set; }
    public string? CostOverName { get; set; }
    public string? CostOverCode { get; set; }
    public long? ServiceId { get; set; }
    public string? ServiceName { get; set; }
    public long? ProjectOperationId { get; set; }
    public string? ProjectOperationName { get; set; }
    public long? ProjectOperationsDetailId { get; set; }
    public string? ProjectOperationsDetailName { get; set; }
    public decimal? Percentage { get; set; }
    public decimal? Amount { get; set; }
    public string? Description { get; set; }
    public long? ProjectOperationsDetailServiceId { get; set; }
    public long? ContractorContractId { get; set; }
    public string? ContractorContractDescription { get; set; }
}

public record GetModeledContractorStatusStatementByIdProduct
{
    public long Id { get; set; }
    public long? RequestGoodsSupplyDetailId { get; set; }
    public decimal? RequestNumber { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public long? ProductGroupId { get; set; }
    public string? MeasureUnit { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public decimal? SupplyCount { get; set; }
    public decimal? Price { get; set; } = 0;

    public decimal? RequestCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TaxNumber { get; set; }
    public decimal? DiscountByNum { get; set; }
    public decimal? OtherPrice { get; set; } = 0;
    public decimal? TransferPrice { get; set; } = 0;
    public decimal? DiscountOnInvoiceNumber { get; set; } = 0;
    public decimal? TotalPrice { get; set; } = 0;
    public string? CustomerInvoiceNumber { get; set; } = string.Empty;
    public DateTime? CreatedDate { get; set; }
    public string? Created => TimeCalculator.DatePiker(CreatedDate);
    public string? CreatedShamsi => CreatedDate.ToShamsi();
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}


public record GetModeledContractorStatusStatementByIdForContractorProduct
{
    public long Id { get; set; }
    public long? RequestGoodsSupplyDetailId { get; set; }
    public decimal? RequestNumber { get; set; }
    public long? ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public long? ProductGroupId { get; set; }
    public string? MeasureUnit { get; set; } = string.Empty;
    public string? OperationInfoName { get; set; } = string.Empty;
    public decimal? SupplyCount { get; set; }
    public decimal? Price { get; set; } = 0;

    public decimal? RequestCount { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TaxNumber { get; set; }
    public decimal? DiscountByNum { get; set; }
    public decimal? OtherPrice { get; set; } = 0;
    public decimal? TransferPrice { get; set; } = 0;
    public decimal? DiscountOnInvoiceNumber { get; set; } = 0;
    public decimal? TotalPrice { get; set; } = 0;
    public string? CustomerInvoiceNumber { get; set; } = string.Empty;
    public DateTime? CreatedDate { get; set; }
    public string? Created => TimeCalculator.DatePiker(CreatedDate);
    public string? CreatedShamsi => CreatedDate.ToShamsi();
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}


public record GetModeledContractorStatusStatementByIdFines
{
    public long Id { get; set; }
    public long RequestRewardId { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public string? RegistrationDateShamsi => RegistrationDate.ToShamsi();
    public string? RequestRewardDescription { get; set; } = string.Empty;
    public string? RequestRewardManagerDescription { get; set; } = string.Empty;
    public decimal OfferedPrice { get; set; }   ///////// Is Confirmed Price , the Front needs to change the description of the field or add another field
    public RequestRewardType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public RequestRewardStatus Status => RequestRewardStatus.Confirmed;
    public string StatusDescription => Status.GetEnumDescription();
    public List<string>? Documents { get; set; }
    public bool HaveDocuments => Documents is not null && Documents.Any() ? true : false;
}

public record GetModeledContractorStatusStatementByIdRewards
{
    public long Id { get; set; }
    public long RequestRewardId { get; set; }
    public string? RegistrationDate { get; set; } = string.Empty;
    public string? RequestRewardDescription { get; set; } = string.Empty;
    public string? RequestRewardManagerDescription { get; set; } = string.Empty;
    public decimal OfferedPrice { get; set; }   ///////// Is Confirmed Price , the Front needs to change the description of the field or add another field
    public RequestRewardType Type => RequestRewardType.Reward;
    public string TypeDescription => Type.GetEnumDescription();
    public RequestRewardStatus Status => RequestRewardStatus.Confirmed;
    public string StatusDescription => Status.GetEnumDescription();
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}

public record GetsCSSContractorContractHeaderModel
{
    public long Id { get; set; }
    public long ContractorId { get; set; }
    public string? Contractor { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; }
    public string? CostCenterCode { get; set; }
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public string? ProjectCode { get; set; }
    public long? ProjectManagerId { get; set; }
    public string? ProjectManager { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? FinalTotalAmount => ContractorContracts.Sum(x => x.TotalAmount);
    public decimal? TotalPercentageDoingJobWell => ContractorContracts.Sum(x => x.PercentageDoingJobWell) / ContractorContracts.Count;
    public decimal? TotalDoingJobWellAmount => ContractorContracts.Sum(x => x.DoingJobWellAmount) / ContractorContracts.Count;
    public decimal? TotalAdvancePaymentAmount => ContractorContracts.Sum(x => x.AdvancePaymentAmount);
    public decimal? TotalPercentageAdvancePayment => (TotalAdvancePaymentAmount / FinalTotalAmount) * 100;
    public decimal? TotalDailyLatenessPenalty => ContractorContracts.Sum(x => x.DailyLatenessPenalty) / ContractorContracts.Count;
    public decimal? TotalWorkDonePercent => ContractorContracts.Sum(x => x.WorkDonePercent) / ContractorContracts.Count;
    public decimal? TotalWorkDeliveryPercent => ContractorContracts.Sum(x => x.WorkDeliveryPercent) / ContractorContracts.Count;
    public decimal? TotalWorkCompletionPercent => ContractorContracts.Sum(x => x.WorkCompletionPercent) / ContractorContracts.Count;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string? Description { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public List<GetsCSSContractorContractModel> ContractorContracts { get; set; } = new();
}

public record GetsCSSContractorContractModel
{
    public long Id { get; set; }
    public long ContractorContractStatusStatementId { get; set; }
    public ContractorContractType ContractorContractTypeId { get; set; }
    public string? ContractorContractTypeName => ContractorContractTypeId.GetEnumDescription();
    public string? ContractorContractTypeCode { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? TotalAmount { get; set; } = 0;
    public decimal? PercentageTotalAmount { get; set; } = 0;
    public decimal? FixedContractPct { get; set; } = 0;

    public string? FixedContractPctDesc { get; set; }

    public decimal? ProjectFixedContractPct { get; set; } = 0;

    public string? ProjectFixedContractPctDesc { get; set; }

    public decimal? ManagerFixedContractPct { get; set; } = 0;

    public string? ManagerFixedContractPctDesc { get; set; }
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public int? WorkDonePercent { get; set; }
    public int? WorkDeliveryPercent { get; set; }
    public int? WorkCompletionPercent { get; set; }
    public string? Description { get; set; }
    public string? ContractorContractType { get; set; }

}

public record GetModeledContractorStatusStatementByIdDetail
{
    public long Id { get; set; }
    public long? ContractorStatusStatementId { get; set; }
    public long? ContractorContractId { get; set; }
    public long? ContractorContractHeaderId { get; set; }
    public string? ContractorContractHeaderDescription { get; set; }
    public string? ContractorContractType { get; set; }
    public string? ContractorContractTypeCode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? StartDateShamsi => StartDate is null ? null : TimeCalculator.ConvertToShamsi(StartDate);
    public string? EndDateShamsi => EndDate is null ? null : TimeCalculator.ConvertToShamsi(EndDate);
    public decimal? TotalAmount { get; set; }
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public int? WorkDonePercent { get; set; }
    public int? WorkDeliveryPercent { get; set; }
    public int? WorkCompletionPercent { get; set; }
    public string? Description { get; set; }
    public List<GetModeledContractorStatusStatementByIdByIdService>? Services { get; set; } = new();
}

public record GetModeledContractorStatusStatementByIdByIdService
{
    public long? Id { get; set; }
    public long? ContractorContractDetailId { get; set; }
    public long? DailyProjectOperationId { get; set; }
    public long? ProjectOperationId { get; set; } = new();
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public long? ProjectOperationUnitOfMeasurementId { get; set; }
    public string? ProjectOperationUnitOfMeasurement { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public long? UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurement { get; set; } = string.Empty;
    public decimal? ThirdPartiesAmount { get; set; }
    public decimal? ProjectManagerApprovalAmount { get; set; }
    public decimal? ManagementApprovalAmount { get; set; }
    public List<GetModeledContractorStatusStatementByIdServiceDailies>? DailyServices { get; set; } = new();
    public List<GetModeledContractorStatusStatementByIdByIdServiceThirdParties>? ThirdParties { get; set; } = new();
}

public record GetModeledContractorStatusStatementByIdServiceDailies
{
    public long? Id { get; set; }
    public long? DailyProjectOperationServiceId { get; set; }
    public long? TimeSpantLong { get; set; }
    public string? TimeSpant { get; set; } = string.Empty;
    public DateTime? DailyDate { get; set; }
    public decimal? Volume { get; set; }
    public decimal? Price { get; set; }
    public decimal? AcceptablePercentage { get; set; } = 0;
    public decimal? AcceptableAmount { get; set; } = 0;
    public string? AcceptableDescription { get; set; } = string.Empty;
    public decimal? ProjectManagementApprovalPercentage { get; set; } = 0;
    public decimal? ProjectManagementApprovedPrice { get; set; } = 0;
    public string? ProjectManagementApprovedDescription { get; set; } = string.Empty;
    public decimal? ManagementApprovalPercentage { get; set; } = 0;
    public decimal? ApprovedPrice { get; set; } = 0;
    public string? ApprovedDescription { get; set; } = string.Empty;
}

public record GetModeledContractorStatusStatementByIdByIdServiceThirdParties
{
    public long? Id { get; set; }
    public long? ContractorStatusStatementServiceId { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; }
    public long? SkillId { get; set; }
    public string? Skill { get; set; }
    public ContractorContractDetailCooperationBasis Type { get; set; }
    public string? RequestRewardDescription => Type.GetEnumDescription();
    public DateTime? WorkingDay { get; set; }
    public decimal? Price { get; set; }
}

public record PaidContractorStatusStatementModel
{
    public long Id { get; set; }
    public long? CategoryId { get; set; }
    public string? CategoryName { get; set; } = string.Empty;
    public long? BranchId { get; set; }
    public string? BranchName { get; set; } = string.Empty;
    public long? SeasonId { get; set; }
    public string? SeasonName { get; set; } = string.Empty;
    public string? Contractor { get; set; } = string.Empty;
    public string? Nickname { get; set; } = string.Empty;
    public string? Currency { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? StartDateShamsi => StartDate is null ? null : TimeCalculator.ConvertToShamsi(StartDate);
    public string? EndDateShamsi => EndDate is null ? null : TimeCalculator.ConvertToShamsi(EndDate);
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
    public decimal? CreatorConfirmedAmount { get; set; }
    public decimal? ProjectManagerConfirmedAmount { get; set; }
    public decimal? ManagementConfirmedAmount { get; set; }
    public string? LastDescription { get; set; } = string.Empty;
    public decimal? ProductsAmount { get; set; }
    public decimal? FinesAmount { get; set; }
    public decimal? RewardsAmount { get; set; }
    public decimal? ThirdPartiesAmount { get; set; }
    public decimal? UserApprovalAmount { get; set; }
    public decimal? ProjectManagerApprovalAmount { get; set; }
    public decimal? ManagementApprovalAmount { get; set; }
    public decimal? PaymentedAmount { get; set; }
    public decimal? DiscountedAmount { get; set; }
    public decimal? ConfirmedPrice { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? ManagmentDescription { get; set; } = string.Empty;
    public bool IsLast { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
}

public record GetContractorStatusStatementByIdDiscounts
{
    public long Id { get; set; }
    public long? RequestRewardId { get; set; }
    public decimal DiscountPrice { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? RequestRewardDescription { get; set; } = string.Empty;
    public string? ManagerDescription { get; set; } = string.Empty;
    public decimal? OfferedPrice { get; set; }
    public decimal Price { get; set; }
    public RequestRewardType Type => RequestRewardType.Discount;
    public string TypeDescription => Type.GetEnumDescription();
    public RequestRewardStatus Status => RequestRewardStatus.Confirmed;
    public string statusDescription => Status.GetEnumDescription();
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => Created.ToShamsi();
    public List<string>? Documents { get; set; }
    public bool HaveDocuments => Documents is not null && Documents.Any() ? true : false;
}

public record GetContractorStatusStatementDetailModel
{
    public long Id { get; set; }
    public long ContractorStatusStatementId { get; set; }
    public long ContractorContractId { get; set; }
    public long ContractorContractHeaderId { get; set; }
    public string? ContractorContractType { get; set; }
    public string? ContractorContractTypeCode { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? PercentageDoingJobWell { get; set; }
    public decimal? DoingJobWellAmount { get; set; }
    public decimal? PercentageAdvancePayment { get; set; }
    public decimal? AdvancePaymentAmount { get; set; }
    public decimal? DailyLatenessPenalty { get; set; }
    public int? WorkDonePercent { get; set; }
    public int? WorkDeliveryPercent { get; set; }
    public int? WorkCompletionPercent { get; set; }
    public string? Description { get; set; }
    public List<GetContractorStatusStatementByIdServiceModel>? Services { get; set; } = new();
}

public record GetContractorStatusStatementByIdServiceModel
{
    public long Id { get; set; }
    public long ContractorContractDetailId { get; set; }
    public long DailyProjectOperationId { get; set; }
    public long? ProjectOperationId { get; set; } = new();
    public string? OperationInfoName { get; set; }
    public string? OperationInfoCode { get; set; }
    public long? ProjectOperationUnitOfMeasurementId { get; set; }
    public string? ProjectOperationUnitOfMeasurement { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public long? UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurement { get; set; } = string.Empty;
    public decimal ThirdPartiesAmount { get; set; }
    public List<GetContractorStatusStatementServiceDailiesModel>? DailyServices { get; set; } = new();
    public List<GetContractorStatusStatementByIdServiceThirdPartiesModel>? ThirdParties { get; set; } = new();
}

public record GetContractorStatusStatementServiceDailiesModel
{
    public long Id { get; set; }
    public long DailyProjectOperationServiceId { get; set; }
    public string? TimeSpant { get; set; } = string.Empty;
    public DateTime? DailyDate { get; set; }
    public decimal Volume { get; set; }
    public decimal Price { get; set; }
    public decimal AcceptablePercentage { get; set; } = 0;
    public decimal AcceptableAmount { get; set; } = 0;
    public string? AcceptableDescription { get; set; } = string.Empty;
    public decimal ProjectManagementApprovalPercentage { get; set; } = 0;
    public decimal ProjectManagementApprovedPrice { get; set; } = 0;
    public string? ProjectManagementApprovedDescription { get; set; } = string.Empty;
    public decimal ManagementApprovalPercentage { get; set; } = 0;
    public decimal ApprovedPrice { get; set; } = 0;
    public string? ApprovedDescription { get; set; } = string.Empty;
}

public record GetContractorStatusStatementByIdServiceThirdPartiesModel
{
    public long Id { get; set; }
    public long ContractorStatusStatementServiceId { get; set; }
    public long ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; }
    public long SkillId { get; set; }
    public string? Skill { get; set; }
    public ContractorContractDetailCooperationBasis Type { get; set; }
    public string? RequestRewardDescription => Type.GetEnumDescription();
    public string? WorkingDay { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
