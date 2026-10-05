
namespace Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelExporter;

public record GetsProjectTypeExcelExporterModel
{
    public long Id { get; set; }
    public string ProjectTypeName { get; set; } = string.Empty;
    public string ProjectTypeCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
};
