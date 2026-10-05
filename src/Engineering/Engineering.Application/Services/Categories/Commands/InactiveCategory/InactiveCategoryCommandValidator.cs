namespace Engineering.Application.Services.Categories.Commands.InactiveCategory;

public class InactiveCategoryCommandValidator : AbstractValidator<InactiveCategoryCommand>
{
    public InactiveCategoryCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}