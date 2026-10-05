namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementById;

public class GetEmployerStatusStatementByIdQueryValidator : AbstractValidator<GetEmployerStatusStatementByIdQuery>
{
    public GetEmployerStatusStatementByIdQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(EmployerStatusStatementErrors.EmployerStatusStatementIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
