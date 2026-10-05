
namespace Engineering.Application.Services.Projects.Models.ProjectStatusChanger;

public record SetProjectStatusToCanceledRequest(
    long Id
     ) : IHttpRequest;
