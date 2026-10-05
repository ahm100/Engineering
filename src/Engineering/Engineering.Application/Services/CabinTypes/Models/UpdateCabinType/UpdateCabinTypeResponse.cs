namespace Engineering.Application.Services.CabinTypes.Models.UpdateCabinType;

public record UpdateCabinTypeResponse(
    long Id,
    string CabinTypeName,
    string CabinTypeCode);