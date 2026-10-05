namespace Engineering.Application.Services.CabinTypes.Models.GetsCabinType;

public record GetsCabinTypeResponseModel
{
    public long Id { get; set; }
    public int CabinTypeCode { get; set; }
    public string CabinTypeName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}