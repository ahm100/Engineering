namespace Engineering.Application.Services.EmployerEmployees.Contracts.GetEmployeesByEmployerId;

public class GetEmployeesByEmployerIdValidator : AbstractValidator<GetEmployeesByEmployerIdRequest>
{
    public GetEmployeesByEmployerIdValidator()
    {
        RuleFor(oo => oo.EmployerId).IsPositive(GlobalCmts.Id);
    }
}