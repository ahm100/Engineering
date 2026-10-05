using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Enum;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Exporter;

public record GetDraftedServiceCCsExporterRequest(
    long ContractorId,
    List<GetDraftedServiceCCsEnum>? ServiceFilters,
    List<GetDraftedServiceDailiesEnum>? DailyFilters,
    long ProjectId,
    DateTime? StartDate,
    DateTime? EndDate,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
