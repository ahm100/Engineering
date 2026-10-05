using Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices.Enum;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices.Exporter;

public record GetConfirmedCCDailyServicesExporterRequest(
     List<long>? Ids,
     List<long>? CostCenterIds,
     List<long>? ProjectIds,
     List<long>? ProjectOperationIds,
     List<long>? ProjectOperationDetailIds,
     List<long>? ContractorIds,
     List<long>? ServiceInfoIds,
     List<long>? MeasurUnitIds,
     List<GetConfirmedCCDailyServicesEnum>? ExcelFilters,
     DateTime? StartDate,
     DateTime? EndDate,
     string? FilterData,
     string? FilterServiceInfo,
     string? ContractorName,
     string[]? OrderBy,
     int PageIndex,
     int PageSize
     ) : IHttpRequest;
