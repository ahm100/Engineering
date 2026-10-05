using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailForContractorServices;

public record GetProjectOperationDetailForContractorServicesQuery(
    long Id
    ) : IQuery<ProjectOperationDetail>;