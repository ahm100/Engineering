namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByName;

public record GetOperationLocationByNameRequest(
    string PrivateName
     ) : IHttpRequest;
