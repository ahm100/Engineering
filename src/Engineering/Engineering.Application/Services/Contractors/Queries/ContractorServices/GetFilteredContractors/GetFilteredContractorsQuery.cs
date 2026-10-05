namespace Engineering.Application.Services.Contractors.Queries.ContractorServices.GetFilteredContractors;

public record GetFilteredContractorsQuery(long ProjectId,
                                            List<long>? ProjectOperationIds) : IQuery<List<long>>;

