
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices;

public record GetConfirmedCCDailyServicesResponse(
    GetConfirmedCCDailyServicesTotalModel OtherData,
    List<GetConfirmedCCDailyServicesModel> Data,
    int RowCount
    );

public record GetConfirmedCCDailyServicesTotalModel
{
    public decimal TotalVolume { get; set; } = 0;
    public decimal DoneVolume { get; set; } = 0;
    public decimal RemainVolume => TotalVolume - DoneVolume;
    public decimal DonePrice { get; set; } = 0;
    public decimal ContractPrice { get; set; } = 0;
}

public record GetConfirmedCCDailyServicesModel
{
    public long Id { get; set; }
    public long ContractorContractDetailId { get; set; }
    public decimal? ContractorContractDetailVolume { get; set; } = 0;
    public decimal? ContractorContractDetailPrice { get; set; } = 0;
    public string ContractorContractCode { get; set; } = string.Empty;
    public decimal? Volume { get; set; } = 0;
    public decimal? TotalServiceVolume { get; set; } = 0;
    public decimal? UnitPrice { get; set; } = 0;
    public decimal? TotalPrice => Volume * UnitPrice;
    public long ServiceInfoId { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public string ServiceInfoCode { get; set; } = string.Empty;
    public long ServiceInfoMeasureId { get; set; }
    public string? ServiceInfoMeasure { get; set; } = string.Empty;

    public long ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;

    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long? ProjectOperationId { get; set; }
    public string? ProjectOperationName { get; set; } = string.Empty;
    public decimal? Workload { get; set; } = 0;
    public long ProjectOperationMeasureId { get; set; }
    public string? ProjectOperationMeasure { get; set; } = string.Empty;
    public long? ProjectOperationDetailId { get; set; }
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Number { get; set; }
    public decimal? FinalAmount => Length * Width * Height * Weight * Number;

    public DateTime? StartDateMiladi { get; set; }
    public string? StartDate => TimeCalculator.ConvertToShamsi(StartDateMiladi);
    public DateTime? EndDateMiladi { get; set; }
    public string? EndDate => TimeCalculator.ConvertToShamsi(EndDateMiladi);

    public long? CreatorId { get; set; }
    public string? CreatorName { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
}
