using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Models.GetFilteredRequestMachineries;

public record GetFilteredRequestMachineriesRequest(long? CostCenterId,
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
                                                   string[]? OrderBy,
                                                   int PageIndex,
                                                   int PageSize) : IHttpRequest;
