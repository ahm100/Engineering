namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocationByCode;

public record GetOperationLocationByCodeRequest(
    string PrivateCode
     ) : IHttpRequest;
