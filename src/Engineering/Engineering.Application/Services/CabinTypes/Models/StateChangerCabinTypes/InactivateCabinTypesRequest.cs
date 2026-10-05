namespace Engineering.Application.Services.CabinTypes.Models.StateChangerCabinTypes;

public record InactivateCabinTypesRequest(
    List<long> Ids)
    : IHttpRequest;