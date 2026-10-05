using Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetsRequestMachineryStatusStatementExcelEnum;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetsRequestMachineryStatusStatementExcelExporter;

public record GetsRequestMachineryStatusStatementExcelExporterRequest(
    List<long>? Ids,
    List<long>? ContractorIds,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? MachineryIds,
    List<RequestMachineryStatusStatementStatus>? Statuses,
    RequestMachineryStatusStatementUnit? Unit,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    List<RequestMachineryStatusStatementExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
