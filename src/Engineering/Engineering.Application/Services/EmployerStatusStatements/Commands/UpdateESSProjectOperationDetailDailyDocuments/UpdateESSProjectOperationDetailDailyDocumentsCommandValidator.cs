
namespace Engineering.Application.Services.EmployerStatusStatements.Commands.UpdateESSProjectOperationDetailDailyDocuments;

public class UpdateESSProjectOperationDetailDailyDocumentsCommandValidator : AbstractValidator<UpdateESSProjectOperationDetailDailyDocumentsCommand>
{
    public UpdateESSProjectOperationDetailDailyDocumentsCommandValidator()
    {
        RuleFor(oo => oo.Entity)
            .NotNull().WithError(EmployerStatusStatementErrors.UnValidId);
    }
}
