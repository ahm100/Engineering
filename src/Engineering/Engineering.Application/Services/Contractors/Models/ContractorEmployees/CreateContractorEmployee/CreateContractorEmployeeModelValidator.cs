namespace Engineering.Application.Services.Contractors.Models.ContractorEmployees.CreateContractorEmployee;

public class CreateContractorEmployeeModelValidator : AbstractValidator<CreateContractorEmployeeRequestModel>
{
    public CreateContractorEmployeeModelValidator()
    {
        RuleFor(c => c.EmployeeId).NotNull().WithError(ContractorEmployeeErrors.InValidEmployeeId);
    }
}