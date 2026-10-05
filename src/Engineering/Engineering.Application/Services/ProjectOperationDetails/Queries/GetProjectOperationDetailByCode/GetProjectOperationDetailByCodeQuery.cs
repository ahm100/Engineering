using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByCode;

public record GetProjectOperationDetailByCodeQuery(
    string Code,
    long? OperationLocationId,
    long? CompanyId
    ) : IQuery<ProjectOperationDetail>;
