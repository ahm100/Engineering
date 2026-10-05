using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Queries.GetRequestMachineryBillById;

public record GetRequestMachineryBillByIdQuery(long RequestMachineryBillId) : IQuery<RequestMachineryBill>;

