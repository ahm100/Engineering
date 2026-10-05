using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;

public record GetFltrEContractsRequest(
    long? HeadId,
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? EmployerIds,
    List<EContractType>? Types,
    List<EContractStatus>? Statuses,
    List<EContractStatus>? RemoveStatuses,
    bool? IsPrimaryManager,
    bool? IsFinalManager,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
