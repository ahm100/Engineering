namespace Engineering.Application.Services.Contractors.Commands.ContractorEmployees.UpdateContractorEmployee;

public class UpdateContractorEmployeeCommandValidator : AbstractValidator<UpdateContractorEmployeeCommand>
{
    public UpdateContractorEmployeeCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().WithError(ContractorEmployeeErrors.InValidId);
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorEmployeeErrors.InValidContractorId);
        RuleFor(c => c.EmployeeId).NotNull().WithError(ContractorEmployeeErrors.InValidEmployeeId);
    }
}