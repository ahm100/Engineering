namespace Engineering.Application.Services.BillOfLadings.Contracts.BillOfLadingExcelImports;

public record BillOfLadingExcelImportsRequest(
    IFormFile DocumentFile)
    : IHttpRequest;

public record BillOfLadingExcelImportsModel
{
    public string BillOfLadingName { get; private set; } = string.Empty;
    public string BillOfLadingCode { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
}