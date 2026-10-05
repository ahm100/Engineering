using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetContractorEmployeesByContractorId;

public record GetContractorEmployeesByContractorIdQuery(
    List<long> Ids,
    List<long>? EmployeeId
    ) : IQuery<DataResult<List<ContractorEmployee>>>;

