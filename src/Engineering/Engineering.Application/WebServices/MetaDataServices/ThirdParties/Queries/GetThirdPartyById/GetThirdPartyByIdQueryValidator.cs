
namespace Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetThirdPartyById;

public class GetThirdPartyByIdQueryValidator : AbstractValidator<GetThirdPartyByIdQuery>
{
    public GetThirdPartyByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}
