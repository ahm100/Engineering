using Engineering.Application.Services.SubProjects.Models;

namespace Engineering.Application.Services.SubProjects.Contracts.GetAllowedSubProjectManagers;

public record GetAllowedSubProjectManagersResponse(List<SubProjectManager> Data);
