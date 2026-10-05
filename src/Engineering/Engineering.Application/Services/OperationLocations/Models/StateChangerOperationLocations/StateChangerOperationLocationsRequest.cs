
namespace Engineering.Application.Services.OperationLocations.Models.StateChangerOperationLocations;

public record StateChangerOperationLocationsRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
