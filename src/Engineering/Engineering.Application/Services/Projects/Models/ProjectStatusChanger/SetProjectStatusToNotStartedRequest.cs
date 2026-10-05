
namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public record SetProjectStatusToNotStartedRequest(
    long Id
     ) : IHttpRequest;
