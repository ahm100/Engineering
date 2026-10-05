namespace Engineering.Application.Services.CostCenterTypes.Queries.GetsCostCenterTypeByIds;

public class GetsCostCenterTypeByIdsQueryValidator : AbstractValidator<GetsCostCenterTypeByIdsQuery>
{
    public GetsCostCenterTypeByIdsQueryValidator()
    {
        RuleFor(v => v.Items)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);
    }
}