namespace Engineering.Application.Services.ServiceInfos.Models.ServiceInfoExcelImports;

public record ServiceInfoExcelImportsModel
{
    public string ServiceInfoName { get; private set; } = string.Empty;
    public string ServiceInfoCode { get; private set; } = string.Empty;
    public long MeasurementId { get; private set; }
    public bool IsActive { get; private set; } = true;
}
