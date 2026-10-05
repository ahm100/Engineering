using Project = Engineering.Domain.Entities.Projects.Project;

namespace Engineering.Application.Services.Projects.Queries.GetProjectByCode;

public record GetProjectByCodeQuery(
    string ProjectCode,
    long? CostCenterId,
    long? CompanyId
    ) : IQuery<Project>;