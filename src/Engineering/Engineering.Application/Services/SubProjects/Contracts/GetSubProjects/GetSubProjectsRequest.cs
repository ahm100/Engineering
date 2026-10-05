using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.SubProjects.Contracts.GetSubProjects;

public record GetSubProjectsRequest(
    long ProjectId,
    string? FilterData,
    SubProjectStatus? Status,
    SubProjectType? Type,
    int PageIndex,
    int PageSize) : IHttpRequest;
