using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs.Enum;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs.Exporter;


public record GetDraftedFixCCsExporterRequest(
    long ContractorId,
    List<GetDraftedFixCCsEnum>? ServiceFilters,
    List<GetDraftedFixDailiesEnum>? DailyFilters,
    long ProjectId,
    DateTime? StartDate,
    DateTime? EndDate,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
