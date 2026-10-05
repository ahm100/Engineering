
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByFilter;

public record GetsContractorServiceByFilterRequest(
    string? FilterData,
    long CostCenterId,
    long ProjectId,
    List<long> ProjectOperationIds,
    List<long> ServiceInfoIds,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
