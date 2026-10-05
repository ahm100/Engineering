namespace Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByName;

public record GetProjectTypeByNameRequest(
    string ProjectTypeName
     ) : IHttpRequest;
