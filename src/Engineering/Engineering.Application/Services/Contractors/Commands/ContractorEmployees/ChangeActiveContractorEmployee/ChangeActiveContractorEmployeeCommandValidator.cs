namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.ChangeActiveContractorEmployee;

public class ChangeActiveContractorEmployeeCommandValidator : AbstractValidator<ChangeActiveContractorEmployeeCommand>
{
    public ChangeActiveContractorEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("شناسه اجباری می باشد.");
    }
}
