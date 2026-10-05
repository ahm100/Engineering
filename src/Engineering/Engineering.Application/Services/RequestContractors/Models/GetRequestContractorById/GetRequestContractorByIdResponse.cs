using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractors.Models.GetRequestContractorById;

public record GetRequestContractorByIdResponse
{
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
    public long? OperationLocationId { get; set; }
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
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    [JsonIgnore]
    public RequestContractorInquiry? ConfirmInquiry { get; set; }
    public long? ContractorId => ConfirmInquiry?.ContractorId;
    public string? ContractorName { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;
    public decimal? TotalAmount => ConfirmInquiry?.TotalAmount;
    public decimal? Amount => ConfirmInquiry?.Amount;
    public decimal? Discount => ConfirmInquiry?.Discount;
    public decimal? Tax => ConfirmInquiry?.Tax;
    public long? CurrencyId => ConfirmInquiry?.CurrencyId;
    public string? CurrencyName { get; set; } = string.Empty;
    public RequestContractorType? Type => ConfirmInquiry?.Type;
    public string? TypeDescription => Type?.GetEnumDescription();
    public DateTime? FromDate => ConfirmInquiry?.FromDate;
    public DateTime? ToDate => ConfirmInquiry?.ToDate;
    public long? ConfirmUser => ConfirmInquiry?.ConfirmedUser;
    public string? ConfirmUserName { get; set; } = string.Empty;
}
