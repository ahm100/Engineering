
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailStatusChanger;

public record SetProjectOperationDetailToDoingRequest(
    long Id
     ) : IHttpRequest;
