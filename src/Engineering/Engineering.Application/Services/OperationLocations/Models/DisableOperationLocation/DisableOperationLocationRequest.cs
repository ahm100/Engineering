namespace Engineering.Application.Services.OperationLocations.Models.DisableOperationLocation;

public record DisableOperationLocationRequest(
    long Id
     ) : IHttpRequest;
