using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.OpAssign.GetForUpdate;

public record GetOpAssignForUpdateQuery(long Id)
    : IQuery<ProjectOperationDetailContractorService>;