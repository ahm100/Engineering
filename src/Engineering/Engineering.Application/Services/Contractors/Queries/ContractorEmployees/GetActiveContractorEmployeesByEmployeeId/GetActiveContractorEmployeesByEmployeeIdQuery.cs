using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetActiveContractorEmployeesByEmployeeId;

public record GetActiveContractorEmployeesByEmployeeIdQuery(long Id) : IQuery<ContractorEmployee>;

