namespace Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeByCode;

public record GetProjectTypeByCodeRequest(
    string ProjectTypeCode
     ) : IHttpRequest;
