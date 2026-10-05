namespace Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;

public record GetCabinTypeByNameRequest(
    string CabinTypeName)
    : IHttpRequest;