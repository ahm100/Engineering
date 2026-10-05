
namespace Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices;

public record GetConfirmedCCDailyServicesRequest(
     List<long>? Ids,
     List<long>? CostCenterIds,
     List<long>? ProjectIds,
     List<long>? ProjectOperationIds,
     List<long>? ProjectOperationDetailIds,
     List<long>? ContractorIds,
     List<long>? ServiceInfoIds,
     List<long>? MeasurUnitIds,
     DateTime? StartDate,
     DateTime? EndDate,
     string? FilterData,
     string? FilterServiceInfo,
     string[]? OrderBy,
     int PageIndex,
     int PageSize
    ) : IHttpRequest;
