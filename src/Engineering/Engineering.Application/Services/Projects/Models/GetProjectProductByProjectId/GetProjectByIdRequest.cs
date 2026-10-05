namespace Engineering.Application.Services.Projects.Models.GetProjectById;

public record GetProjectProductByProjectIdRequest(
    long Id
     ) : IHttpRequest;
