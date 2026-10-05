namespace Engineering.Application.Services.SubProjects.Contracts.GetAllowedSubProjectManagers;

public record GetAllowedSubProjectManagersRequest(long ProjectId) : IHttpRequest;
