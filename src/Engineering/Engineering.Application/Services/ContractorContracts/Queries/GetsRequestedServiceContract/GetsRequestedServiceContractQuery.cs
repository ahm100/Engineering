using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsRequestedServiceContract;

public record GetsRequestedServiceContractQuery(
    long ProjectId,
    List<long> ServiceIds,
    long ContractorId,
    long? CompanyId
    ) : IQuery<DataResult<List<ProjectOperationDetailContractorService>>>;
