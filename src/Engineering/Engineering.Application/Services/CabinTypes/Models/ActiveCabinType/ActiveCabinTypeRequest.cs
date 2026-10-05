namespace Engineering.Application.Services.CabinTypes.Models.ActiveCabinType;

public record ActiveCabinTypeRequest(
    long Id)
    : IHttpRequest;