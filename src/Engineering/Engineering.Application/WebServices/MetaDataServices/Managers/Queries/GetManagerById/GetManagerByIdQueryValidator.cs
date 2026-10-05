
namespace Engineering.Application.WebServices.MetaDataServices.Managers.Queries.GetManagerById;

public class GetManagerByIdQueryValidator : AbstractValidator<GetManagerByIdQuery>
{
    public GetManagerByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}