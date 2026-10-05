
namespace Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoExcelExporter;

public record GetsOperationInfoExcelExporterModel
{
    public long Id { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string? OperationLatinName { get; set; }
    public int? Priority { get; set; }
    public long UnitOfMeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public long? OperationInfoDependencyId { get; set; }
    public long? RelationId { get; set; }
    public string? DependencyName { get; set; }
    public string? DependencyCode { get; set; }
    public int? DependencyPriority { get; set; }
    public int? WorkingDays { get; set; }
    public string? DependencyType { get; set; }
    public string? Categories { get; set; }
    public string? Branchs { get; set; }
    public string? Seasons { get; set; }
    public string? Groups { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;

};
