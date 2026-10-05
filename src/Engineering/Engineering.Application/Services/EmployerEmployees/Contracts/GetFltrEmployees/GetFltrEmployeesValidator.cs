namespace Engineering.Application.Services.EmployerEmployees.Contracts.GetFltrEmployees;

public class GetFltrEmployeesValidator : AbstractValidator<GetFltrEmployeesRequest>
{
    public GetFltrEmployeesValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}