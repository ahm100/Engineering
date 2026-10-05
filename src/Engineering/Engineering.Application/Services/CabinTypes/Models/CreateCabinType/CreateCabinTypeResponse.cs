namespace Engineering.Application.Services.CabinTypes.Models.CreateCabinType;

public record CreateCabinTypeResponse(
    long Id,
    string CabinTypeCode,
    string CabinTypeName);