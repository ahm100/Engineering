
namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public record SetProjectStatusToClosedRequest(
    long Id
     ) : IHttpRequest;
