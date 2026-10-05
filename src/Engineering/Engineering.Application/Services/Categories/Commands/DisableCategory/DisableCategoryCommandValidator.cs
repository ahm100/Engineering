namespace Engineering.Application.Services.Categories.Commands.DisableCategory;

public class DisableCategoryCommandValidator : AbstractValidator<DisableCategoryCommand>
{
    public DisableCategoryCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}