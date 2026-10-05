using ProjectType = Engineering.Domain.Entities.Projects.ProjectType;

namespace Engineering.Application.Services.ProjectTypes.Queries.GetActiveProjectTypes;

public record GetActiveProjectTypesQuery(
    string? FilterData,
    string? code,
    string? name,
    long? CompanyId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectType>>>;