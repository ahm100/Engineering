namespace Engineering.Application.Services.Categories.Models.GetsCategoryExcelExporter;

public record GetsCategoryExcelExporterModel
{
    public long Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
}