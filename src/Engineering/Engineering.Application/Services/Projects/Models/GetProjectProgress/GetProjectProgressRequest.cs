namespace Engineering.Application.Services.Projects.Models.GetProjectProgress;

public record GetProjectProgressRequest(
    long Id
     ) : IHttpRequest;