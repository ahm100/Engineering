namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailByCode;

public record GetProjectOperationDetailByCodeRequest(
    string Code,
    long? OperationLocationId
     ) : IHttpRequest;
