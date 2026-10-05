namespace Engineering.Application.Services.Projects.Models.GetSummarizedProjectById;

public record GetSummarizedProjectByIdRequest(
    long Id
     ) : IHttpRequest;
