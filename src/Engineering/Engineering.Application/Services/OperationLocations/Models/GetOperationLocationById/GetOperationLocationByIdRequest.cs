namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationById;

public record GetOperationLocationByIdRequest(
    long Id
     ) : IHttpRequest;
