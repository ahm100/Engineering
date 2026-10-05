
namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public record SetProjectStatusToStoppedRequest(
    long Id
     ) : IHttpRequest;
