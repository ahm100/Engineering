namespace Engineering.Application.Services.EmployerEmployees.Commands.CreateEmployerEmployee;

public class CreateEmployerEmployeeCommandValidator : AbstractValidator<CreateEmployerEmployeeCommand>
{
    public CreateEmployerEmployeeCommandValidator()
    {
        RuleFor(oo => oo.EmployeeId).IsPositive(GlobalCmts.EmployeeId);
        RuleFor(oo => oo.EmployerId).IsPositive(EContractCmts.EmployerId);
        RuleFor(oo => oo.IsActive).IsRequiredBool(GlobalCmts.IsActive);
    }
}