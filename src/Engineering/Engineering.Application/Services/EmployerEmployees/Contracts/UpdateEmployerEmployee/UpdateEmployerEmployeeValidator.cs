namespace Engineering.Application.Services.EmployerEmployees.Contracts.UpdateEmployerEmployee;

public class UpdateEmployerEmployeeValidator : AbstractValidator<UpdateEmployerEmployeeRequest>
{
    public UpdateEmployerEmployeeValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}