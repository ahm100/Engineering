namespace Engineering.Application.Services.Projects.Models.Delete;

public record DeleteProjectRequest(
    long Id
     ) : IHttpRequest;
