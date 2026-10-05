using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsRequestedOperationContract;

public record GetsRequestedOperationContractQuery(
    List<long>? ProjectOperationDetailServiceIds,
    long? CompanyId
    ) : IQuery<DataResult<List<ProjectOperationDetailContractorService>>>;
