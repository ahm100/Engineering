
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;

public record SetProjectOperationToEndOfWorkRequest(
    long Id
     ) : IHttpRequest;
