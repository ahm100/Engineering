using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailForValidates;

public record GetProjectOperationDetailForValidatesQuery(
    long ProjectOperationId,
    long OperationLocationId,
    string Code,
    long? CompanyId
    ) : IQuery<ProjectOperationDetail>;