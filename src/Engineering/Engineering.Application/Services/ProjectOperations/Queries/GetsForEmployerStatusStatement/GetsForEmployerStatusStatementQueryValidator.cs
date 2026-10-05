namespace Engineering.Application.Services.ProjectOperations.Queries.GetsForEmployerStatusStatement;

public class GetsForEmployerStatusStatementQueryValidator : AbstractValidator<GetsForEmployerStatusStatementQuery>
{
    public GetsForEmployerStatusStatementQueryValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .NotNull().WithError(ProjectOperationErrors.OperationInfoIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.ProjectId)
            .NotNull().WithError(ProjectOperationErrors.ProjectIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(oo => oo.StartDate)
            .NotEmpty().WithError(ProjectOperationErrors.StartDateIsEmpty);

        RuleFor(oo => oo.EndDate)
            .NotEmpty().WithError(ProjectOperationErrors.EndDateIsEmpty);

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