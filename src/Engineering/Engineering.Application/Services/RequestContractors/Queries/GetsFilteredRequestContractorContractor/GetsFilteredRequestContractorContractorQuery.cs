namespace Engineering.Application.Services.RequestContractors.Queries.GetsFilteredRequestContractorContractor;

public record GetsFilteredRequestContractorContractorQuery(List<long>? CostCenterIds, List<long>? ProjectIds) : IQuery<DataResult<List<long>>>;
