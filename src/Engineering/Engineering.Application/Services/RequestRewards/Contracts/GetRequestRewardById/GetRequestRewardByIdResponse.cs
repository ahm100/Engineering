using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Contracts.GetRequestRewardById;

public record GetRequestRewardByIdResponse
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public GetRequestRewardByIdCostCenterModel CostCenter { get; set; } = new();
    public GetRequestRewardByIdProjectModel? Project { get; set; }
    public GetRequestRewardByIdProjectOperationModel? ProjectOperation { get; set; }
    public GetRequestRewardByIdProjectOperationDetailModel? ProjectOperationDetail { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public RequestRewardType Type { get; set; }
    public string TypeDescriptionn => Type.GetEnumDescription();
    public RequestRewardStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public List<GetRequestRewardByIdThirdPartyModel>? ThirdParties { get; set; } = new();
    public decimal? OfferedPrice { get; set; }
    public decimal ConfirmedPrice { get; set; }
    public string? ManagerDescription { get; set; }
    public GetRequestRewardByIdCurrencyModel? Currency { get; set; } = new();
    public DateTime RegistrationDate { get; set; }
    public List<GetRequestRewardByIdDocumentModel>? Documents { get; set; } = new();
    public List<GetRequestRewardByIdProductModel>? Products { get; set; } = new();
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
