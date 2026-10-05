namespace Engineering.Application.Services.Projects.Models.GetProjectByCode;

public record GetProjectByCodeRequest(
    string ProjectCode
     ) : IHttpRequest;
