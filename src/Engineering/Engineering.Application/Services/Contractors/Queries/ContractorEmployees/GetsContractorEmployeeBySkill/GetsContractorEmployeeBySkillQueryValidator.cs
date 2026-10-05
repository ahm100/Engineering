
namespace Engineering.Application.Services.Contractors.Queries.ContractorEmployees.GetsContractorEmployeeBySkill;

public class GetsContractorEmployeeBySkillQueryValidator : AbstractValidator<GetsContractorEmployeeBySkillQuery>
{
    public GetsContractorEmployeeBySkillQueryValidator()
    {
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorServicesErrors.ContractorIdIsEmpty);
    }
}
