
namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDocuments;

public class UpdateESSProjectOperationDocumentsCommandValidator : AbstractValidator<UpdateESSProjectOperationDocumentsCommand>
{
    public UpdateESSProjectOperationDocumentsCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(EmployerStatusStatementErrors.UnValidId);
    }
}
