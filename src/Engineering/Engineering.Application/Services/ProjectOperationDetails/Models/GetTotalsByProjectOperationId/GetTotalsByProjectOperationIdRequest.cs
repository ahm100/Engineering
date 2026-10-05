namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetTotalsByProjectOperationId;

public record GetTotalsByProjectOperationIdRequest(
    long ProjectOperationId
     ) : IHttpRequest;
