
namespace Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelExporter;

public record GetsOperationInfoGroupExcelExporterModel
{
    public long Id { get; set; }
    public string OperationInfoGroupName { get; set; } = string.Empty;
    public string OperationInfoGroupCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
};
