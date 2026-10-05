namespace Engineering.Application.Services.CabinTypes.Queries.GetsCabinTypeByIds;

public class GetsCabinTypeByIdsQueryValidator : AbstractValidator<GetsCabinTypeByIdsQuery>
{
    public GetsCabinTypeByIdsQueryValidator()
    {
        RuleFor(v => v.Items)
            .NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}