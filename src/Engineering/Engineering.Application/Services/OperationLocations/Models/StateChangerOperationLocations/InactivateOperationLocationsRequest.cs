
namespace Engineering.Application.Services.OperationLocations.Models.StateChangerOperationLocations;

public record InactivateOperationLocationsRequest(
    List<long> Ids
    ) : IHttpRequest;
