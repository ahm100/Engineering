namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.CreateContractorEmployee;

public class CreateContractorEmployeeValidator : AbstractValidator<CreateContractorEmployeeRequest>
{
    public CreateContractorEmployeeValidator()
    {
        RuleFor(c => c.ContractorId).NotNull().WithError(ContractorEmployeeErrors.InValidContractorId);
        RuleForEach(c => c.Employees).NotEmpty().SetValidator(new CreateContractorEmployeeModelValidator()).WithError(ContractorEmployeeErrors.InValidEmployeeId);
    }
}
