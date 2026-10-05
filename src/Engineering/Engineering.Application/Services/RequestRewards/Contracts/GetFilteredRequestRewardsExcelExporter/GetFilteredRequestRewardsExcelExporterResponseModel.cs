using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelExporter;

public record GetFilteredRequestRewardsExcelExporterResponseModel
{
    public long Id { get; set; }
    public RequestRewardStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public RequestRewardType Type { get; set; }
    public string TypeDescription => Type.GetEnumDescription();
    public string ThirdParties { get; set; } = string.Empty;
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
    public string? ContractCode { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public string? RegistrationDateShamsi => RegistrationDate.ToShamsi();
    public decimal? OfferedPrice { get; set; }
    public decimal ConfirmedPrice { get; set; }
    public string? ManagerDescription { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public List<GetRequestRewardThirdPartyModel>? ThirdPartiesModel { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}

