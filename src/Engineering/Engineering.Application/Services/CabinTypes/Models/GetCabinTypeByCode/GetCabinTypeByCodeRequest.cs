namespace Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;

public record GetCabinTypeByCodeRequest(
    int CabinTypeCode)
    : IHttpRequest;