namespace Engineering.Application.Services.Categories.Commands.StateChangerCategories;

public class StateChangerCategoriesCommandValidator : AbstractValidator<StateChangerCategoriesCommand>
{
    public StateChangerCategoriesCommandValidator()
    {
        RuleFor(v => v.Items)
            .NotEmpty().WithError(GlobalErrors.IdsIsEmpty)
            .NotNull().WithError(GlobalErrors.IdsIsNull);
    }
}