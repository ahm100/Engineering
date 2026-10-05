
namespace Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelExporter;

public record GetsServiceInfoExcelExporterModel
{
    public long Id { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public string ServiceInfoCode { get; set; } = string.Empty;
    public long? MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
};
