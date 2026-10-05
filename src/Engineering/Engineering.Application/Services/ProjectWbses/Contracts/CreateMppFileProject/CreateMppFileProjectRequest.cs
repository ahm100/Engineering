namespace Engineering.Application.Services.ProjectWbses.Contracts.CreateMppFileProject;

public record CreateMppFileProjectRequest(
    long ProjectId) : IHttpRequest;