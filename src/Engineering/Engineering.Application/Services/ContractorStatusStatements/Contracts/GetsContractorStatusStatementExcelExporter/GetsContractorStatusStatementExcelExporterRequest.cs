using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelEnum;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelExporter;

public record GetsContractorStatusStatementExcelExporterRequest(
    List<long>? Ids,
    long? ContractorId,
    long? CostCenterId,
    long? ProjectId,
    long? ProjectManagerId,
    long? ContractorContractHeaderId,
    string? ContractNumber,
    string? Code,
    string? ManagerAmount,
    string? ManagerDescription,
    List<CSSStatus>? Statuses,
    bool IsPayment,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    List<ContractorStatusStatementExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
