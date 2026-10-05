namespace Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;

public record GetProjectServiceByIdRequest(
    long Id
     ) : IHttpRequest;
