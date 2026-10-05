
namespace Engineering.Application.WebServices.MetaDataServices.Planers.Queries.GetPlanerById;

public class GetPlanerByIdQueryValidator : AbstractValidator<GetPlanerByIdQuery>
{
    public GetPlanerByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}