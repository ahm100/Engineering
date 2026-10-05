namespace Engineering.Application.Services.EmployerEmployees.Contracts.DeleteEmployerEmployee;

public class DeleteEmployerEmployeeValidator : AbstractValidator<DeleteEmployerEmployeeRequest>
{
    public DeleteEmployerEmployeeValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}