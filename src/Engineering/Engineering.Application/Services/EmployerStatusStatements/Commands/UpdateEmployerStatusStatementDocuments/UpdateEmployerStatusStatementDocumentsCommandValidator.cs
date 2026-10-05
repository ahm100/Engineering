
namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateEmployerStatusStatementDocuments;

public class UpdateEmployerStatusStatementDocumentsCommandValidator : AbstractValidator<UpdateEmployerStatusStatementDocumentsCommand>
{
    public UpdateEmployerStatusStatementDocumentsCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(EmployerStatusStatementErrors.UnValidId);
    }
}
