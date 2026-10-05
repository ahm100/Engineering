using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByName;

public record GetProjectByNameQuery(
    string ProjectName,
    long? CostCenterId,
    long? CompanyId
    ) : IQuery<Project>;