namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetEmployerStatusStatementExcelExporter;

public class GetEmployerStatusStatementExcelExporterQueryValidator : AbstractValidator<GetEmployerStatusStatementExcelExporterQuery>
{
    public GetEmployerStatusStatementExcelExporterQueryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithError(EmployerStatusStatementErrors.EmployerStatusStatementIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
