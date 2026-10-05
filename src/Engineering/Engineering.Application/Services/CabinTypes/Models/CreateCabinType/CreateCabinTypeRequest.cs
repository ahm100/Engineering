namespace Engineering.Application.Services.CabinTypes.Models.CreateCabinType;

public record CreateCabinTypeRequest(
    int CabinTypeCode,
    string CabinTypeName,
    bool IsActive)
    : IHttpRequest;