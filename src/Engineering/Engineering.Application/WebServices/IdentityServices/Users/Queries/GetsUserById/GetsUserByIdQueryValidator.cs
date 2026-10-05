
namespace Engineering.Application.IdentityServices.Users.Queries.GetsUserById;

public class GetsUserByIdQueryValidator : AbstractValidator<GetsUserByIdQuery>
{
    public GetsUserByIdQueryValidator()
    {
        RuleFor(oo => oo.Ids).NotEmpty().WithError(MetaDataErrors.IdIsEmpty);
    }
}