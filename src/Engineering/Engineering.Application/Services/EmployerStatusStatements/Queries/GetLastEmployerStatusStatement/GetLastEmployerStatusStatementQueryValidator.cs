namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetLastEmployerStatusStatement;

public class GetLastEmployerStatusStatementQueryValidator : AbstractValidator<GetLastEmployerStatusStatementQuery>
{
    public GetLastEmployerStatusStatementQueryValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .NotNull().WithError(EmployerStatusStatementErrors.CostCenterIdIsEmpty).GreaterThanOrEqualTo(1)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.ProjectId)
            .NotNull().WithError(EmployerStatusStatementErrors.ProjectIdEmpty).GreaterThanOrEqualTo(1)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}