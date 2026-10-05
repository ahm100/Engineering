
namespace Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetCitiesByCodes;

public class GetCitiesByCodesQueryValidator : AbstractValidator<GetCitiesByCodesQuery>
{
    public GetCitiesByCodesQueryValidator()
    {
        RuleFor(oo => oo.Codes).NotEmpty().WithError(MetaDataErrors.CodeIsEmpty);
    }
}