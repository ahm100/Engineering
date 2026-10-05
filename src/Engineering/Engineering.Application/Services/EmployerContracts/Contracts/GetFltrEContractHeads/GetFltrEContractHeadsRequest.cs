using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;

public record GetFltrEContractHeadsRequest(
    long? HeadId,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? EmployerIds,
    List<EContractType>? Types,
    List<EContractStatus>? Statuses,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
