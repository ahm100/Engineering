namespace Engineering.Application.Services.CabinTypes.Models.GetsActiveCabinTypes;

public record GetsActiveCabinTypeModel
{
    public long Id { get; set; }
    public int CabinTypeCode { get; set; }
    public string CabinTypeName { get; set; } = string.Empty;
}