namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetEmployerStatusStatementLimitDate;

public class GetEmployerStatusStatementLimitDateQueryValidator : AbstractValidator<GetEmployerStatusStatementLimitDateQuery>
{
    public GetEmployerStatusStatementLimitDateQueryValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(EmployerStatusStatementErrors.ProjectIdEmpty);
        RuleFor(oo => oo.EmployerContractId).NotNull().GreaterThanOrEqualTo(1).WithError(EmployerStatusStatementErrors.EmployerContractIdEmpty);
    }
}
