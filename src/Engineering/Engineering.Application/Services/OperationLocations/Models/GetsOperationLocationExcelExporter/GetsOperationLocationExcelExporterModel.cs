namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelExporter;

public record GetsOperationLocationExcelExporterModel
{
    public long Id { get; set; }
    public string PrivateName { get; set; } = string.Empty;
    public string PrivateCode { get; set; } = string.Empty;
    public string PublicName { get; set; } = string.Empty;
    public string PublicCode { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Coding { get; set; } = string.Empty;
    public string OperationLocationInfo { get; set; } = string.Empty;
    public long? ParentId { get; set; }
    public string? ParentPublicName { get; set; } = string.Empty;
    public string? ParentPublicCode { get; set; } = string.Empty;
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public int? Priority { get; set; }
    public bool IsActive { get; set; }
    public bool HaveChild { get; set; }
    public int ChildCount { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
};
