
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationStatusChanger;

public record SetProjectOperationStatusToDefiniteDeliveryRequest(
    long Id
     ) : IHttpRequest;
