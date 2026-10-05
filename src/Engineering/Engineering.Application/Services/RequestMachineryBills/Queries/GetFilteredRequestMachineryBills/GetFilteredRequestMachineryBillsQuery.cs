using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Queries.GetFilteredRequestMachineryBills;

public record GetFilteredRequestMachineryBillsQuery(
        List<long>? Ids,
        long? RequestMachineryId,
        long? CostCenterId,
        long? ProjectId,
        List<long>? ContractorIds,
        List<long>? ProjectOperationIds,
        long? MachineriesGroupId,
        long? MachineryId,
        DateTime? FromDate,
        DateTime? ToDate,
        long? CreatorId,
        int? BillNumber,
        string? FilterData,
        long? CompanyId,
        string[]? OrderBy,
        int PageIndex,
        int PageSize) : IQuery<DataResult<List<RequestMachineryBill>>>;
