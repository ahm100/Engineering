
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;

public record SetProjectOperationToTemporaryDeliveryRequest(
    long Id
     ) : IHttpRequest;
