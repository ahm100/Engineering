
namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public record SetProjectStatusToTemporaryDeliveryRequest(
    long Id
     ) : IHttpRequest;
