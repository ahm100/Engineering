namespace Engineering.Application.Services.RequestMachineryBills.Models.GetFilteredRequestMachineryBills;

public record GetFilteredRequestMachineryBillsRequest(
                                                   long? RequestMachineryId,
                                                   long? CostCenterId,
                                                   long? ProjectId,
                                                   List<long>? ContractorIds,
                                                   List<long>? ProjectOperationIds,
                                                   long? MachineriesGroupId,
                                                   long? MachineryId,
                                                   long? CreatorId,
                                                   DateTime? FromDate,
                                                   DateTime? ToDate,
                                                   string? FilterData,
                                                   string[]? OrderBy,
                                                   int PageIndex,
                                                   int PageSize) : IHttpRequest;
