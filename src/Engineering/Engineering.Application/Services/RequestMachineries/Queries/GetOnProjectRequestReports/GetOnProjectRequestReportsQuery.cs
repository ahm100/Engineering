using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetOnProjectRequestReports;

public record GetOnProjectRequestReportsQuery(
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
    long? CompanyId,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<RequestMachinery>>>;
