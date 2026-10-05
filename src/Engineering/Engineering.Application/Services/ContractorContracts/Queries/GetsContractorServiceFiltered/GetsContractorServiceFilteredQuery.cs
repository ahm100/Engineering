using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorServiceFiltered;

public record GetsContractorServiceFilteredQuery(
    List<long>? ServiceIds,
    List<long>? ProjectOperationDetailServiceIds,
    long? ProjectId,
    long? ContractorId,
    List<long>? ProjectOperationIds,
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetailContractorService>>>;
