
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;

public record SetProjectOperationToStoppedRequest(
    long Id
     ) : IHttpRequest;
