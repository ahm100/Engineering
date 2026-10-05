namespace Engineering.Application.Services.Categories.Commands.ActiveCategory;

public class ActiveCategoryCommandValidator : AbstractValidator<ActiveCategoryCommand>
{
    public ActiveCategoryCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}