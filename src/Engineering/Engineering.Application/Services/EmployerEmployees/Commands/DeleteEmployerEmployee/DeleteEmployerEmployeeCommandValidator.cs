namespace Engineering.Application.Services.EmployerEmployees.Commands.DeleteEmployerEmployee;

public class DeleteEmployerEmployeeCommandValidator : AbstractValidator<DeleteEmployerEmployeeCommand>
{
    public DeleteEmployerEmployeeCommandValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}