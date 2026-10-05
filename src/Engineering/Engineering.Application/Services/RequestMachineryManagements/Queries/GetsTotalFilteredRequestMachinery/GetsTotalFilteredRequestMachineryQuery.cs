using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetsTotalFilteredRequestMachinery;

public record GetsTotalFilteredRequestMachineryQuery(
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
    long? OperatorAppoinmentUserId,
    int? RequestNumber,
    string? DriverName,
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IQuery<DataResult<List<RequestMachinery>>>;
