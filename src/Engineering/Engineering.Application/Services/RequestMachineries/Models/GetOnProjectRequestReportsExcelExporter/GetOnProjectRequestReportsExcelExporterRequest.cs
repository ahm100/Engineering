using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelEnums;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelExporter;

public record GetOnProjectRequestReportsExcelExporterRequest(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long? ContractorId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? MachineryIds,
    RequestMachineryUnit? Unit,
    RequestMachineryPaymentType? PaymentType,
    DateTime? StartDate,
    DateTime? EndDate,
    DateTime? ConfirmedFromDate,
    DateTime? ConfirmedToDate,
    string? FilterData,
    bool? OwnCompany,
    string[]? OrderBy,
    List<GetsOnProjectRequestExcelEnum>? ExcelFilters,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
