using Engineering.Domain.Entities.ContractorEmployees;

namespace Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetsContractorEmployeeBySkill;

public record GetsContractorEmployeeBySkillQuery(
    long ContractorId,
    long? CompanyId
    ) : IQuery<DataResult<List<ContractorEmployee>>>;
