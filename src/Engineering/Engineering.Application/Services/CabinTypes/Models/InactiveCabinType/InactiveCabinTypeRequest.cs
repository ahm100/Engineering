namespace Engineering.Application.Services.CabinTypes.Models.InactiveCabinType;

public record InactiveCabinTypeRequest(
    long Id)
    : IHttpRequest;