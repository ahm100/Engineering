namespace Engineering.Application.Services.CabinTypes.Models.StateChangerCabinTypes;

public record StateChangerCabinTypesRequest(
    List<long> Ids,
    bool State)
    : IHttpRequest;