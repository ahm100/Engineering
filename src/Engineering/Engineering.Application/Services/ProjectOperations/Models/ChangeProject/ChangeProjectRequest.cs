namespace Engineering.Application.Services.ProjectOperations.Models.ChangeProject;

public record ChangeProjectRequest(
    long Id,
    long ProjectId
    ) : IHttpRequest;