
namespace Engineering.Application.WebServices.Commercial.Commerces.Commands.RemoveCommerce;

public class RemoveCommerceCommandValidator : AbstractValidator<RemoveCommerceCommand>
{
    public RemoveCommerceCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(MetaDataErrors.IdIsEmpty);
    }
}
