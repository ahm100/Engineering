namespace Engineering.Application.Services.ProjectServices.Models.InactiveProjectService;

public record InactiveProjectServiceRequest(
    long Id
     ) : IHttpRequest;
