
namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public record SetProjectStatusToEndOfWorkRequest(
    long Id
     ) : IHttpRequest;
