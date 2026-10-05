namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementLetterheadExcel;

public class GetEmployerStatusStatementLetterheadExcelQueryValidator : AbstractValidator<GetEmployerStatusStatementLetterheadExcelQuery>
{
    public GetEmployerStatusStatementLetterheadExcelQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(EmployerStatusStatementErrors.EmployerStatusStatementIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
