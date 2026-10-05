namespace Engineering.Application.Services.EmployerStatusStatements.Models.CreateEmployerStatusStatement;

public class CreateEmployerStatusStatementValidator : AbstractValidator<CreateEmployerStatusStatementRequest>
{
    public CreateEmployerStatusStatementValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(EmployerStatusStatementErrors.ProjectIdEmpty);

        RuleFor(oo => oo.EmployerContractId).NotNull().GreaterThanOrEqualTo(1).WithError(EmployerStatusStatementErrors.EmployerContractIdEmpty);

        RuleForEach(oo => oo.ProjectOperations).NotNull().WithError(EmployerStatusStatementErrors.ProjectOperationIdEmpty);

        RuleFor(oo => oo.StartDate).NotNull().NotEmpty().WithError(EmployerStatusStatementErrors.StartDateEmpty);

        RuleFor(oo => oo.EndDate).NotNull().NotEmpty().WithError(EmployerStatusStatementErrors.EndDateEmpty);
    }
}
