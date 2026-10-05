
namespace Engineering.Application.WebServices.MetaDataServices.Experts.Queries.GetExpertById;

public class GetExpertByIdQueryValidator : AbstractValidator<GetExpertByIdQuery>
{
    public GetExpertByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}