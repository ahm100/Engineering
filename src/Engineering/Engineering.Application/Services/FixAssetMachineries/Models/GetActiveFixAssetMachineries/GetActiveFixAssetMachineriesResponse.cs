
using Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryModel;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Models.GetActiveFixAssetMachineries;

public record GetActiveFixAssetMachineriesResponse(
    List<GetsActiveFixAssetMachineryModel> Data,
    int RowCount
    );

public record GetsActiveFixAssetMachineryModel
{
    public long Id { get; set; }
    public long MachineryId { get; set; }
    public string MachineryName { get; set; } = string.Empty;
    public string MachineryCode { get; set; } = string.Empty;
    public FixAssetMachineryType FixAssetMachineryType { get; set; }
    public string TypeDesctiption => FixAssetMachineryType.GetEnumDescription();
    public long? DriverId { get; set; }
    public string? DriverFullname { get; set; } = string.Empty;
    public string? DriverName { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public string? Description { get; set; } = string.Empty;
    public string? MachinerySpecification { get; set; } = string.Empty;
    public string? NumberPlates { get; set; } = string.Empty;
    public NumberPlatesModel? NumberPlatesModel { get; set; } = new();
    public decimal? MachineryPrice { get; set; }
    public decimal? HourlyRate { get; set; }
    public decimal? DailyRate { get; set; }
    public decimal? ServiceRate { get; set; }
    public decimal? volumeRate { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyName { get; set; } = string.Empty;

}