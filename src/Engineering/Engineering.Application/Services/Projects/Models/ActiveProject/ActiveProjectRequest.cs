namespace Engineering.Application.Services.Projects.Models.ActiveProject;

public record ActiveProjectRequest(
    long Id
     ) : IHttpRequest;
