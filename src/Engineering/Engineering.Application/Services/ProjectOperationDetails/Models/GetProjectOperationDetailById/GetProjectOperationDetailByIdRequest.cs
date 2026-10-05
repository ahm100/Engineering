
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailById;

public record GetProjectOperationDetailByIdRequest(
    long Id
     ) : IHttpRequest;
