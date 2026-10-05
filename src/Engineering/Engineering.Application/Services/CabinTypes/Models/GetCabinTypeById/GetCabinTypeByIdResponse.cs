namespace Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;

public record GetCabinTypeByIdResponse
{
    public long Id { get; set; }
    public int CabinTypeCode { get; set; }
    public string CabinTypeTitle { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}