using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetSendManagerMachineryReports;

public record GetSendManagerMachineryReportsQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long? ContractorId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? MachineryIds,
    RequestMachineryUnit? Unit,
    List<RequestMachineryStatus>? Statuses,
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
