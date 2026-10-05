
namespace Engineering.Application.WebServices.Commercial.Commerces.Commands.CreateCommerce;

public class CreateCommerceCommandValidator : AbstractValidator<CreateCommerceCommand>
{
    public CreateCommerceCommandValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(MetaDataErrors.IdIsEmpty);
    }
}