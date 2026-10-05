namespace Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;

public record GetCabinTypeByNameResponse
{
    public long Id { get; set; }
    public int CabinTypeCode { get; set; }
    public string CabinTypeTitle { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}