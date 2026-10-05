namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.GetsContractorEmployeeBySkill;

public class GetsContractorEmployeeBySkillValidator : AbstractValidator<GetsContractorEmployeeBySkillRequest>
{
    public GetsContractorEmployeeBySkillValidator()
    {
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorServicesErrors.ContractorIdIsEmpty);
        RuleFor(c => c.SkillId).NotNull().WithError(ContractorServicesErrors.SkillIdIsEmpty);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
    }
}
