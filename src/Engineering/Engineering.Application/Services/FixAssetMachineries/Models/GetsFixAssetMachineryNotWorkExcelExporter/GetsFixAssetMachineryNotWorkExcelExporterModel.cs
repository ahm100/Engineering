
using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;

public record GetsFixAssetMachineryNotWorkExcelExporterModel
{
    public long Id { get; set; }
    public long FixAssetMachineryId { get; set; }
    public string MachineryName { get; set; } = string.Empty;
    public string MachineryCode { get; set; } = string.Empty;
    public FixAssetMachineryType FixAssetMachineryType { get; set; }
    public string TypeDesctiption => FixAssetMachineryType.GetEnumDescription();
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public string? FromDateShamsi => TimeCalculator.ConvertToShamsi(FromDate);
    public DateTime ToDate { get; set; }
    public string? ToDateShamsi => TimeCalculator.ConvertToShamsi(ToDate);
    public string? FromTime { get; set; }
    public string? ToTime { get; set; }
    public string? Description { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(ToDate);
    public long? CompanyId { get; set; }
    public string? Company { get; set; }
};