namespace Engineering.Application.Services.Contractors.Queries.ContractorServices.GetContractorsByServiceIds;

public record GetContractorsByServiceIdsQuery(List<long> Ids) : IQuery<List<long>>;

