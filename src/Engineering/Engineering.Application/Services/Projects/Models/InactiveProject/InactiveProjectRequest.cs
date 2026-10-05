namespace Engineering.Application.Services.Projects.Models.InactiveProject;

public record InactiveProjectRequest(
    long Id
     ) : IHttpRequest;
