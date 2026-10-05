namespace Engineering.Application.Services.RequestMachineries.Queries.GetsFilteredMachineryContractor;

public record GetsFilteredMachineryContractorQuery(List<long>? CostCenterIds, List<long>? ProjectIds) : IQuery<DataResult<List<long>>>;
