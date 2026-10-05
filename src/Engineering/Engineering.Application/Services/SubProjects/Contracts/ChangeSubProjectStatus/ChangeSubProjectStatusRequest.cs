using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.SubProjects.Contracts.ChangeSubProjectStatus;

public record ChangeSubProjectStatusRequest(long Id, SubProjectStatus Status) : IHttpRequest;
