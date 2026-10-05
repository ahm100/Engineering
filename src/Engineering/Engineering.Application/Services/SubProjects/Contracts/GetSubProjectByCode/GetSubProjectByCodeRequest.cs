namespace Engineering.Application.Services.SubProjects.Contracts.GetSubProjectByCode;

public record GetSubProjectByCodeRequest(string Code) : IHttpRequest;
