namespace Engineering.Application.Services.CabinTypes.Models.CabinTypeExcelImports;

public record CabinTypeExcelImportsModel
{
    public string CabinTypeName { get; private set; } = string.Empty;
    public int CabinTypeCode { get; private set; }
    public bool IsActive { get; private set; }
}