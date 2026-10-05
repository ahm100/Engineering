namespace Engineering.Application.Services.EmployerEmployees.Queries.GetEmployeesByEmployerId;

public class GetEmployeesByEmployerIdQueryValidator : AbstractValidator<GetEmployeesByEmployerIdQuery>
{
    public GetEmployeesByEmployerIdQueryValidator()
    {
        RuleFor(oo => oo.EmployerId).IsPositive(GlobalCmts.Id);
    }
}