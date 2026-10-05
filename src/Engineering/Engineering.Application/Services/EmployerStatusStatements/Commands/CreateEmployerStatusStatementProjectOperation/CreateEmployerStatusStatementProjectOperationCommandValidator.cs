namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatementProjectOperation;

public class CreateEmployerStatusStatementProjectOperationCommandValidator : AbstractValidator<CreateEmployerStatusStatementProjectOperationCommand>
{
    public CreateEmployerStatusStatementProjectOperationCommandValidator()
    {
        RuleFor(oo => oo.EmployerStatusStatementProjectOperation).NotEmpty().WithError(EmployerStatusStatementErrors.EmployerStatusStatementIdEmpty);
    }
}
