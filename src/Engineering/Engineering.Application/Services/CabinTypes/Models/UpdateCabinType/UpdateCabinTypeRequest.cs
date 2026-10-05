namespace Engineering.Application.Services.CabinTypes.Models.UpdateCabinType;

public record UpdateCabinTypeRequest(
    long Id,
    string CabinTypeName,
    bool IsActive)
    : IHttpRequest;