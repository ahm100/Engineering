namespace Engineering.Application.Services.CabinTypes.Models.DeleteCabinType;

public record DeleteCabinTypeRequest(
    long Id)
    : IHttpRequest;