
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;

public record SetProjectOperationStatusToDoingRequest(
    long Id
     ) : IHttpRequest;
