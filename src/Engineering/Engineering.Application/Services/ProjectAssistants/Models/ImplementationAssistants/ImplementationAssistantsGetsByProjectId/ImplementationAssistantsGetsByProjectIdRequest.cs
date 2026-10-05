namespace Engineering.Application.Services.ProjectAssistants.Models.ImplementationAssistants.ImplementationAssistantsGetsByProjectId;

public record ImplementationAssistansGetsByProjectIdRequest(
    long ProjectId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
