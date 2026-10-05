
namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEmployers;

public record GetFltrEmployersRequest(
    string? FilterData,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
