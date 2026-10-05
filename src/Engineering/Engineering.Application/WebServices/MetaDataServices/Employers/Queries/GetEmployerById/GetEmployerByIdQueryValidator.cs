
namespace Engineering.Application.WebServices.MetaDataServices.Employers.Queries.GetEmployerById;

public class GetEmployerByIdQueryValidator : AbstractValidator<GetEmployerByIdQuery>
{
    public GetEmployerByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}