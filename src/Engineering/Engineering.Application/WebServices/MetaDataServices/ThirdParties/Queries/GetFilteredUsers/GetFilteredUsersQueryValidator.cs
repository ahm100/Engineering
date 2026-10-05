namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredUsers;

public class GetFilteredUsersQueryValidator : AbstractValidator<GetFilteredUsersQuery>
{
    public GetFilteredUsersQueryValidator()
    {
        RuleForEach(oo => oo.UserId).NotNull().WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}