
namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReporting;

public record GetsProjectOperationEmployerReportingRequest(
    List<long>? Ids,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ContractorIds,
    List<long>? EmployerIds,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
