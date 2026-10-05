namespace Engineering.Application.Services.ProjectTypes.Models.GetProjectTypeById;

public record GetProjectTypeByIdRequest(
    long Id
     ) : IHttpRequest;
