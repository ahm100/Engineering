namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementByIdForDocs;

public class GetEmployerStatusStatementByIdForDocsQueryValidator : AbstractValidator<GetEmployerStatusStatementByIdForDocsQuery>
{
    public GetEmployerStatusStatementByIdForDocsQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(EmployerStatusStatementErrors.EmployerStatusStatementIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
