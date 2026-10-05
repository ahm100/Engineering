
namespace Engineering.Application.IdentityServices.Users.Queries.GetUsersByRoleIds;

public class GetUsersByRoleIdsQueryValidator : AbstractValidator<GetUsersByRoleIdsQuery>
{
    public GetUsersByRoleIdsQueryValidator()
    {
        RuleFor(oo => oo.RoleIds).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.UserIds).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid).LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid).LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
    }
}
