using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.SubProjects.Contracts.ChangeSubProjectStatus;

public record ChangeSubProjectStatusResponse(long Id, SubProjectStatus Status);
