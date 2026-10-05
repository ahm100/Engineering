namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.CreateContractorEmployee;

public class CreateContractorEmployeeCommandValidator : AbstractValidator<CreateContractorEmployeeCommand>
{
    public CreateContractorEmployeeCommandValidator()
    {
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorEmployeeErrors.InValidContractorId);
        RuleFor(c => c.EmployeeId).NotNull().WithError(ContractorEmployeeErrors.InValidEmployeeId);
    }
}