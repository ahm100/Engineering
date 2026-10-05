namespace Engineering.Application.WebServices.Commercial.Commerces.Commands.CreateTypeCommerce;

public class CreateTypeCommerceCommandValidator : AbstractValidator<CreateTypeCommerceCommand>
{
    public CreateTypeCommerceCommandValidator()
    {
        RuleFor(oo => oo.ProjectId).
            IsPositiveWithNullableInput(MetaDataErrors.IdIsEmpty);
    }
}