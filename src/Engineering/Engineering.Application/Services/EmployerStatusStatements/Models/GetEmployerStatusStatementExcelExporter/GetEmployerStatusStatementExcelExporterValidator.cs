namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementExcelExporter;

public class GetEmployerStatusStatementExcelExporterValidator : AbstractValidator<GetEmployerStatusStatementExcelExporterRequest>
{
    public GetEmployerStatusStatementExcelExporterValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(EmployerStatusStatementErrors.UnValidId);
    }
}
