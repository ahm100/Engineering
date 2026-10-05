namespace Engineering.Application.Services.RequestMachineries.Queries.GetOperationContractors;

public record GetOperationContractorsQuery(long? requestMachineryId) : IQuery<DataResult<List<long>>>;
