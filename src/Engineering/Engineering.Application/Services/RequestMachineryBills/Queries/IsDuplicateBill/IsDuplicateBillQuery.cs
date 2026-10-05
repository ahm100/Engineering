namespace Engineering.Application.Services.RequestMachineryBills.Queries.IsDuplicateBill;

public record IsDuplicateBillQuery(long RequestMachineryId, DateTime FromDate, DateTime ToDate) : IQuery<bool?>;

