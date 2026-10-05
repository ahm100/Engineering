using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsByProjectOperationIdInEmployerContract;

public record GetsByProjectOperationIdInEmployerContractQuery(
    long ProjectOperationId
    ) : IQuery<DataResult<List<ProjectOperationDetail>>>;