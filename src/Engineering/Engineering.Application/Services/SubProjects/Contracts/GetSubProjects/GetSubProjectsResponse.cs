using Engineering.Application.Services.SubProjects.Models;

namespace Engineering.Application.Services.SubProjects.Contracts.GetSubProjects;

public record GetSubProjectsResponse(List<SubProjectDetails> Data, int RowCount);
