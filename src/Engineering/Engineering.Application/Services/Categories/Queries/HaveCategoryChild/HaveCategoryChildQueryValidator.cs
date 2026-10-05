namespace Engineering.Application.Services.Categories.Queries.HaveCategoryChild;

public class HaveCategoryChildQueryValidator : AbstractValidator<HaveCategoryChildQuery>
{
    public HaveCategoryChildQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}