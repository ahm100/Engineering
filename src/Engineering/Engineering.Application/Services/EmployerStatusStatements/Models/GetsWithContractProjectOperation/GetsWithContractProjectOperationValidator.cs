namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsWithContractProjectOperation;

public class GetsWithContractProjectOperationValidator : AbstractValidator<GetsWithContractProjectOperationRequest>
{
    public GetsWithContractProjectOperationValidator()
    {
        RuleFor(c => c.ProjectId)
            .NotNull().WithError(EmployerStatusStatementErrors.ProjectIdEmpty)
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
