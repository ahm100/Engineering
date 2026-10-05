namespace Engineering.Application.Services.EmployerEmployees.Contracts.CreateEmployerEmployee;

public class CreateEmployerEmployeeValidator : AbstractValidator<CreateEmployerEmployeeRequest>
{
    public CreateEmployerEmployeeValidator()
    {
        RuleFor(oo => oo.EmployeeId).IsPositive(GlobalCmts.EmployeeId);
        RuleFor(oo => oo.EmployerId).IsPositive(EContractCmts.EmployerId);
        RuleFor(oo => oo.IsActive).IsRequiredBool(GlobalCmts.IsActive);
    }
}