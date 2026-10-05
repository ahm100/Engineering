
namespace Engineering.Application.WebServices.MetaDataServices.Directors.Queries.GetDirectorById;

public class GetDirectorByIdQueryValidator : AbstractValidator<GetDirectorByIdQuery>
{
    public GetDirectorByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}