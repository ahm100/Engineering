namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;

public record GetDailyProjectOperationByIdSeviceModel
{
    public long Id { get; set; }
    public long ContractorServiceId { get; set; }
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; }
    public decimal Volume { get; set; }
    public decimal? ProjectServiceVolume { get; set; }
    public decimal? ProjectServiceSumVolume { get; set; }
    public decimal? ProjectServiceSumDoneVolume { get; set; }
    public decimal? ProjectServiceSumRemaindDoneVolume { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public string ServiceInfoName { get; set; } = string.Empty;
    public string ServiceInfoCode { get; set; } = string.Empty;
    public long? UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurement { get; set; } = string.Empty;
    public string? TimeSpant { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
