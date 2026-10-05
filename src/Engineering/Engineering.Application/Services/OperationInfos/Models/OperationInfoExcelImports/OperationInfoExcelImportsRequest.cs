namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoExcelImports;

public record OperationInfoExcelImportsRequest(
    IFormFile DocumentFile)
    : IHttpRequest;

public record OperationInfoExcelImportsModel
{
    public string OperationInfoCode { get; set; } = string.Empty;
    public string OperationInfo { get; set; } = string.Empty;
    public string UnitOfMeasurementName { get; set; } = string.Empty;
    public decimal BasePrice { get; set; } = 0;
    public string SeasonName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
}