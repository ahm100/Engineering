namespace Engineering.Application.Services.OperationLocations.Models.SetPriority;

public record SetOperationLocationPriorityRequest(
    long Id,
    int Priority
     ) : IHttpRequest;
