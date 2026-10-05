namespace Engineering.Application.Services.EmployerEmployees.Commands.UpdateEmployerEmployee;

public class UpdateEmployerEmployeeCommandValidator : AbstractValidator<UpdateEmployerEmployeeCommand>
{
    public UpdateEmployerEmployeeCommandValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}