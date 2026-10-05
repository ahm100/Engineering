using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsProjectOperationDetailContractorService;

public record GetsProjectOperationDetailContractorServiceQuery(
    List<long>? ServiceIds,
    List<long>? ProjectServiceIds,
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