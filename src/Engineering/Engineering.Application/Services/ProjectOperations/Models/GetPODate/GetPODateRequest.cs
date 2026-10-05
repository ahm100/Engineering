namespace Engineering.Application.Services.ProjectOperations.Models.GetPODate;

public record GetPODateRequest(
    long ProjectOperationId
     ) : IHttpRequest;