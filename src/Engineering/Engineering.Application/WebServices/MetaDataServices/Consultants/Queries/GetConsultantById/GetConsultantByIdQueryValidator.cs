
namespace Engineering.Application.WebServices.MetaDataServices.Consultants.Queries.GetConsultantById;

public class GetConsultantByIdQueryValidator : AbstractValidator<GetConsultantByIdQuery>
{
    public GetConsultantByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}