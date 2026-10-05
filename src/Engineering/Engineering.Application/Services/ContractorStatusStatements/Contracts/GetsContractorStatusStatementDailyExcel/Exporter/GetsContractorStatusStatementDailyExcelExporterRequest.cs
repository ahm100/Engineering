
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetsContractorStatusStatementDailyExcel.Enum;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementDailyServiceExcelExporter;

public record GetContractorStatusStatementDailyServiceExcelExporterRequest(
     long ContractorStatusStatementId,
     List<ContractorStatusStatementDailyServiceExcelEnum>? ExcelFilters,
     long? ContractorContractHeaderId,
     long? ContractorContractId,
     long? ContractTypeId,
     List<long>? ProjectOperationIds,
     List<long>? ProjectOperationMeasurUnitIds,
     List<long>? ProjectOperationDetailIds,
     List<long>? ServiceInfoIds,
     List<long>? ServiceInfoMeasurUnitIds,
     DateTime? StartDate,
     DateTime? EndDate,
     string? FilterProjectOperation,
     string? FilterDescription,
     string? FilterServiceInfo,
     string[]? OrderBy,
     int PageIndex,
     int PageSize
    ) : IHttpRequest;
