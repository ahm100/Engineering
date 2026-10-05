
namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public record SetProjectStatusToDoingRequest(
    long Id
     ) : IHttpRequest;
