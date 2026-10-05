using Engineering.Domain.Entities.Projects.Enums;
using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetsContractedProject;

public record GetsContractedProjectQuery(
    List<long>? CostCenterIds,
    ProjectStatus? Status,
    string? FilterData,
    long? EmployerId,
    long? ProjectTypeId,
    long? CategoryId,
    bool? IsActive,
    string[]? OrderBy,
    List<ProjectStatus>? Statuses,
    bool HaveCostCenter,
    bool IsOrganizationUnit,
    int PageIndex,
    int PageSize,
    long CompanyId
    ) : IQuery<DataResult<List<Project>>>;
