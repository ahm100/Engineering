
namespace Engineering.Application.Services.ProjectOperations.Models.GetsByProjectOperationId;

public record GetsByProjectOperationIdRequest(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
