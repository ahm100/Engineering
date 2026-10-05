namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatement;

public class CreateEmployerStatusStatementCommandValidator : AbstractValidator<CreateEmployerStatusStatementCommand>
{
    public CreateEmployerStatusStatementCommandValidator()
    {
        RuleFor(oo => oo.Entity).NotEmpty().WithError(EmployerStatusStatementErrors.UnValidId);
    }
}
