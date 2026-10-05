namespace Engineering.Application.Services.EmployerStatusStatements.Commands.CreateEmployerStatusStatementProjectOperationDetailDaily;

public class CreateEmployerStatusStatementProjectOperationDetailDailyCommandValidator : AbstractValidator<CreateEmployerStatusStatementProjectOperationDetailDailyCommand>
{
    public CreateEmployerStatusStatementProjectOperationDetailDailyCommandValidator()
    {
        RuleFor(oo => oo.StatementProjectOperationDetail).NotEmpty().WithError(EmployerStatusStatementErrors.ProjectIdEmpty);
        RuleFor(oo => oo.DailyProjectOperation).NotEmpty().WithError(EmployerStatusStatementErrors.StartDateEmpty);
    }
}
