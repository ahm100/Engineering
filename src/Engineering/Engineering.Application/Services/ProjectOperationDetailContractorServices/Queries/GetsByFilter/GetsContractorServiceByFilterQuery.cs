using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsByFilter;

public record GetsContractorServiceByFilterQuery(
    string? FilterData,
    long CostCenterId,
    long ProjectId,
    List<long> ProjectOperationIds,
    List<long> ServiceInfoIds,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ProjectOperationDetailContractorService>>>;