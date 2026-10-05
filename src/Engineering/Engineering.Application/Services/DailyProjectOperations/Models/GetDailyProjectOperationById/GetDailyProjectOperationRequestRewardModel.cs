using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;

public record GetDailyProjectOperationRequestRewardModel
{
    public long Id { get; set; }
    public RequestRewardType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public long CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public long? ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public List<GetRequestRewardThirdPartyModel>? ThirdPartyIds { get; set; } = new();
    public decimal? OfferedPrice { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;
    public List<GetRequestRewardDocumentModel>? Documents { get; set; }
    public List<GetRequestRewardProductModel>? Products { get; set; } = new();
}

public record GetRequestRewardProductModel
{
    public long? Id { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public long CurrencyId { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Price { get; set; }
}
