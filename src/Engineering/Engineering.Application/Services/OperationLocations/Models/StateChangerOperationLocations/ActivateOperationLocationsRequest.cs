
namespace Engineering.Application.Services.OperationLocations.Models.StateChangerOperationLocations;

public record ActivateOperationLocationsRequest(
    List<long> Ids
    ) : IHttpRequest;
