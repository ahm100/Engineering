namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementById;

public class GetEmployerStatusStatementByIdValidator : AbstractValidator<GetEmployerStatusStatementByIdRequest>
{
    public GetEmployerStatusStatementByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(EmployerStatusStatementErrors.UnValidId);
    }
}
