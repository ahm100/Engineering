
namespace Engineering.Application.WebServices.MetaDataServices.Contractors.Queries.GetContractorById;

public class GetContractorByIdQueryValidator : AbstractValidator<GetContractorByIdQuery>
{
    public GetContractorByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}