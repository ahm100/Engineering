
namespace Engineering.Application.WebServices.MetaDataServices.Banks.Queries.GetBankById;

public class GetBankByIdQueryValidator : AbstractValidator<GetBankByIdQuery>
{
    public GetBankByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MetaDataErrors.IdIsEmpty);
    }
}