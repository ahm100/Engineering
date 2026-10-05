namespace Engineering.Application.Services.DailyProjectOperations.Models.GetEmployerStatusStatementLimitDate;

public class GetEmployerStatusStatementLimitDateValidator : AbstractValidator<GetEmployerStatusStatementLimitDateRequest>
{
    public GetEmployerStatusStatementLimitDateValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(EmployerStatusStatementErrors.ProjectIdEmpty);
        RuleFor(oo => oo.EmployerContractId).NotNull().GreaterThanOrEqualTo(1).WithError(EmployerStatusStatementErrors.EmployerContractIdEmpty);
    }
}
