namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsFilteredEmployerStatusStatement;

public class GetsFilteredEmployerStatusStatementValidator : AbstractValidator<GetsFilteredEmployerStatusStatementRequest>
{
    public GetsFilteredEmployerStatusStatementValidator()
    {
        RuleFor(oo => oo.EmployerId)
            .NotNull().WithError(EmployerStatusStatementErrors.EmployerIdEmpty).GreaterThanOrEqualTo(1)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.CostCenterId)
            .NotNull().WithError(EmployerStatusStatementErrors.CostCenterIdIsEmpty).GreaterThanOrEqualTo(1)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.ProjectId)
            .NotNull().WithError(EmployerStatusStatementErrors.ProjectIdEmpty).GreaterThanOrEqualTo(1)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.EmployerContractId)
            .NotNull().WithError(EmployerStatusStatementErrors.ContractCodeEmpty).GreaterThanOrEqualTo(1)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.PageIndex)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);

        RuleFor(oo => oo.PageSize)
            .GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);

        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex)
                .GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
