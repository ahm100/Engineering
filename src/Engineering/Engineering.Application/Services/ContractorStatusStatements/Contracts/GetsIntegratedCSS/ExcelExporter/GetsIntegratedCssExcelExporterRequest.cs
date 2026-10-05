using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSS.ExcelExporter
{
    public record GetsIntegratedCSSExcelExporterRequest(
    long? ContractorId,
    long? CostCenterId,
    long? ProjectId,
    long? ContractorContractHeaderId,
    List<CSSStatus>? Statuses,
    bool IsPrimaryManager,
    bool IsFinalManager,
    string? ContractorFilter,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
}
