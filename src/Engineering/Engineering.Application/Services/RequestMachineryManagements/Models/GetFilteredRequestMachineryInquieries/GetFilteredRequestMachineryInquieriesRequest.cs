using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetFilteredRequestMachineryInquieries;

public record GetFilteredRequestMachineryInquieriesRequest(long? CostCenterId,
                                                           long? ProjectId,
                                                           List<long>? ContractorIds,
                                                           RequestMachineryStatus? Status,
                                                           DateTime? StartDate,
                                                           DateTime? EndDate,
                                                           DateTime? ConfirmedFromDate,
                                                           DateTime? ConfirmedToDate,
                                                           int? RequestNumber,
                                                           string? FilterData,
                                                           string[]? OrderBy,
                                                           int PageIndex,
                                                           int PageSize) : IHttpRequest;
