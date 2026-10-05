
namespace Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetMeasureunitById;

public class GetMeasureunitByIdQueryValidator : AbstractValidator<GetMeasureunitByIdQuery>
{
    public GetMeasureunitByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}