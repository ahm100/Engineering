using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByContractor;

public record GetProjectOperationDetailByContractorQuery(
    long ProjectId,
    long ContractorId
    ) : IQuery<ProjectOperationDetail>;