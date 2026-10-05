using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredProjectOperations;

public record GetFilteredProjectOperationsQuery(
    List<long> ProjectOperationIds
    ) : IQuery<DataResult<List<ProjectOperation>>>;
