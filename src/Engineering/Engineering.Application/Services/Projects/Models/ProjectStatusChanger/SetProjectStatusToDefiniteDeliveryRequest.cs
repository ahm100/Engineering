
namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public record SetProjectStatusToDefiniteDeliveryRequest(
    long Id
     ) : IHttpRequest;
