using Engineering.Application.Services.RequestMachineryManagements.Models.RequestMachineryManagementExcelEnums;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetsRequestMachineryManagementExcelExporter;

public record GetsRequestMachineryManagementExcelExporterRequest(
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
   long? CreatorId,
   string? DriverName,
   string? FilterData,
   List<RequestMachineryManagementExcelEnum>? ExcelFilters,
   string[]? OrderBy,
   int PageIndex,
   int PageSize
    ) : IHttpRequest;
