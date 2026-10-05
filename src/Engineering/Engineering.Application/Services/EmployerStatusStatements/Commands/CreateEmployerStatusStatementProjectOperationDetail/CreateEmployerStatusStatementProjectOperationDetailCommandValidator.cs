namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatementProjectOperationDetail;

public class CreateEmployerStatusStatementProjectOperationDetailCommandValidator : AbstractValidator<CreateEmployerStatusStatementProjectOperationDetailCommand>
{
    public CreateEmployerStatusStatementProjectOperationDetailCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetail).NotEmpty().WithError(EmployerStatusStatementErrors.ProjectIdEmpty);
        RuleFor(oo => oo.StatementProjectOperation).NotEmpty().WithError(EmployerStatusStatementErrors.StartDateEmpty);
    }
}
