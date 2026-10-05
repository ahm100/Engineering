namespace Engineering.Application.Services.ProjectOperationDetails.Models.PODetailExcelImport;

public record PODetailExcelImportRequest(
IFormFile DocumentFile)
: IHttpRequest;

public record PODetailExcelImportModel
{
    public string ProjectCode { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string OperationLocationCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Lenght { get; set; } = 0;
    public decimal Width { get; set; } = 0;
    public decimal Height { get; set; } = 0;
    public decimal Weight { get; set; } = 0;
    public decimal Number { get; set; } = 0;
}