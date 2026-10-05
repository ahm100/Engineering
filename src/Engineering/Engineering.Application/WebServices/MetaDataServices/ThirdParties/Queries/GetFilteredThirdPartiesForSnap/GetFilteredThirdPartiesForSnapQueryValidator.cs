namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredThirdPartiesForSnap;

public class GetFilteredThirdPartiesForSnapQueryValidator : AbstractValidator<GetFilteredThirdPartiesForSnapQuery>
{
    public GetFilteredThirdPartiesForSnapQueryValidator()
    {
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}