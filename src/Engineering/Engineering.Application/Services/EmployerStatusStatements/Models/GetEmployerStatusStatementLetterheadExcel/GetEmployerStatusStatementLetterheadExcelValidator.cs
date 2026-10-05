namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementLetterheadExcel;

public class GetEmployerStatusStatementLetterheadExcelValidator : AbstractValidator<GetEmployerStatusStatementLetterheadExcelRequest>
{
    public GetEmployerStatusStatementLetterheadExcelValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(EmployerStatusStatementErrors.UnValidId);
    }
}
