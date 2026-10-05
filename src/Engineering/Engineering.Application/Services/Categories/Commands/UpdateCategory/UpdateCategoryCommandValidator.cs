namespace Engineering.Application.Services.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(v => v.CategoryName)
            .NotEmpty().WithError(CategoryErrors.CategoryNameIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);

        RuleFor(v => v.CategoryCode)
            .NotEmpty().WithError(CategoryErrors.CategoryCodeIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}