using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Enum;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Exporter;
public record GetFltrEContractsExporterRequest(
    long? HeadId,
    List<long>? Ids,
    List<FltrEContractsEnum>? ExcelFilters,
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
