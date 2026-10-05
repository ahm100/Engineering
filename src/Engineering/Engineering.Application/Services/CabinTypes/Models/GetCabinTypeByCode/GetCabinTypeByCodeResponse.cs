namespace Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;

public record GetCabinTypeByCodeResponse
{
    public long Id { get; set; }
    public int CabinTypeCode { get; set; }
    public string CabinTypeName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}