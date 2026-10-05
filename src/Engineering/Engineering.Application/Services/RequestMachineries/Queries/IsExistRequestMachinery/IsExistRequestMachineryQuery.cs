namespace Engineering.Application.Services.RequestMachineries.Queries.IsExistRequestMachinery;

public record IsExistRequestMachineryQuery(long ProjectId, long MachineryId, decimal TimeRequired, int RequestCount, long? CompanyId) : IQuery<bool>;

