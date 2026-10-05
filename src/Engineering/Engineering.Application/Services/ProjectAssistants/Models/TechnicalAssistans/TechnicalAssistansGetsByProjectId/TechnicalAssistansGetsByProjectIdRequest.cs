namespace Engineering.Application.Services.ProjectAssistants.Models.TechnicalAssistans.TechnicalAssistansGetsByProjectId;

public record TechnicalAssistansGetsByProjectIdRequest(
    long ProjectId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
