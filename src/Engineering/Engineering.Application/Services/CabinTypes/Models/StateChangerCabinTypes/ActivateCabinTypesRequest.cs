namespace Engineering.Application.Services.CabinTypes.Models.StateChangerCabinTypes;

public record ActivateCabinTypesRequest(
    List<long> Ids)
    : IHttpRequest;