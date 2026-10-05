namespace Engineering.Application.Services.Projects.Models.GetProjectById;

public record GetProjectByIdRequest(
    long Id
     ) : IHttpRequest;

public record GetPdfProjectByIdRequest(
    long Id
     ) : IHttpRequest;