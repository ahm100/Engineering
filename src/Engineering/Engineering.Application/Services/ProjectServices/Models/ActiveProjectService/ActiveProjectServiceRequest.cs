namespace Engineering.Application.Services.ProjectServices.Models.ActiveProjectService;

public record ActiveProjectServiceRequest(
    long Id
     ) : IHttpRequest;
