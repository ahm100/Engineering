
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;

public record SetProjectOperationToNotStartedRequest(
    long Id
     ) : IHttpRequest;
