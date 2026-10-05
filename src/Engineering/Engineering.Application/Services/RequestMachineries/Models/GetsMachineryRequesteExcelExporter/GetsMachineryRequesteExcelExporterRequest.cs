using Engineering.Application.Services.RequestMachineries.Models.GetsEmployerStatusStatementExcelEnums;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.GetsMachineryRequesteExcelExporter;

public record GetsMachineryRequesteExcelExporterRequest(
    List<long>? Ids,
    long? CostCenterId,
    long? ProjectId,
    List<long>? ContractorIds,
    List<long>? ProjectOperationIds,
    List<long>? OperationInfoIds,
    long? MachineriesGroupId,
    long? MachineryId,
    RequestMachineryStatus? Status,
    RequestMachineryPaymentType? PaymentType,
    DateTime? FromDate,
    DateTime? ToDate,
    DateTime? ConfirmedFromDate,
    DateTime? ConfirmedToDate,
    string? DriverName,
    string? FilterData,
    List<GetsRequestMachineryExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
