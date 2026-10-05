using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsIntegratedProjectOperationDetailService;

public record GetsIntegratedProjectOperationDetailServiceQuery(
    List<long>? ProjectOperationDetailServiceIds,
    long? CostCenterId,
    long? ProjectId,
    long? ContractorId,
    List<long>? ProjectOperationIds,
    List<long>? ServiceInfoIds,
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetailContractorService>>>;
