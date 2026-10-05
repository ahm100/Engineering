namespace Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelExporter;

public record GetsCabinTypeExcelExporterModel
{
    public long Id { get; set; }
    public string CabinTypeName { get; set; } = string.Empty;
    public int CabinTypeCode { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
};
