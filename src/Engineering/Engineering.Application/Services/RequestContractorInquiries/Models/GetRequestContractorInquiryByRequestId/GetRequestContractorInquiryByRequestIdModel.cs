using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractorInquiries.Models.GetRequestContractorInquiryByRequestId;

public record GetRequestContractorInquiryByRequestIdModel
{
    public long Id { get; set; }
    public long RequestContractorId { get; set; }
    public RequestContractorStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public long? RequestNumber { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public long? ProjectOperationDetailId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public decimal? Volume { get; set; }
    public string? Description { get; set; }
    public string? DescriptionStatus { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public long? ContractorId { get; set; }
    public string? ContractorName { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;
    public decimal? TotalAmount { get; set; }
    public decimal? Amount { get; set; }
    public decimal? Discount { get; set; }
    public decimal? Tax { get; set; }
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; } = string.Empty;
    public RequestContractorType? Type { get; set; }
    public string? TypeDescription => Type?.GetEnumDescription();
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public long? ConfirmUser { get; set; }
    public string? ConfirmUserName { get; set; } = string.Empty;
    public bool? IsConfirmed { get; set; }
    public string? InquiryDescription { get; set; }
    public long? InquiryCreatorId { get; set; }
    public string? InquiryCreator { get; set; } = string.Empty;
    public DateTime? InquiryCreated { get; set; }
    public List<InquiryDocumentResponseByRequestIdModel>? InquiryDocuments { get; set; }
}

public record InquiryDocumentResponseByRequestIdModel
{
    public long Id { get; set; }
    public string? Url { get; set; }
}
