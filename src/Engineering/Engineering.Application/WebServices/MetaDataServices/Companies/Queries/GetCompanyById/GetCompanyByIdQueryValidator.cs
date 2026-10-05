namespace Engineering.Application.WebServices.MetaDataServices.Companies.Queries.GetCompanyById;

public class GetCompanyByIdQueryValidator : AbstractValidator<GetCompanyByIdQuery>
{
    public GetCompanyByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
           .WithError(MetaDataErrors.IdIsEmpty);
    }
}