
namespace Engineering.Application.Services.OperationLocations.Models.OperationLocationGroupDelete;

public record OperationLocationGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
