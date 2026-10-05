namespace Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;

public record GetsBillOfLadingExcelExporterResponse(
    FileContentResult File);

public record GetsBillOfLadingExcelExporterModel
{
    public long Id { get; set; }
    public string BillOfLadingName { get; set; } = string.Empty;
    public string BillOfLadingCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}