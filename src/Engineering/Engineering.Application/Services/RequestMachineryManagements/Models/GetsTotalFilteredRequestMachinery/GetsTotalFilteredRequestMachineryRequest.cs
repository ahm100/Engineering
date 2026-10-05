using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetsTotalFilteredRequestMachinery;

public record GetsTotalFilteredRequestMachineryRequest(
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
    string? FilterData) : IHttpRequest;
