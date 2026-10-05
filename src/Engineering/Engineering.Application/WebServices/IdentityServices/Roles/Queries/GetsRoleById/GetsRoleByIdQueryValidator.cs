
namespace Engineering.Application.IdentityServices.Roles.Queries.GetsRoleById;

public class GetsRoleByIdQueryValidator : AbstractValidator<GetsRoleByIdQuery>
{
    public GetsRoleByIdQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
    }
}