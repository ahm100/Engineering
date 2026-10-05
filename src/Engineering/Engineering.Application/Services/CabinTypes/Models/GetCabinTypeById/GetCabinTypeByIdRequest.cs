namespace Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;

public record GetCabinTypeByIdRequest(
    long Id)
    : IHttpRequest;