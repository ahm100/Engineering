namespace Engineering.Application.Services.Categories.Queries.GetCategoryWithoutInclude;

public class GetCategoryWithoutIncludeQueryValidator : AbstractValidator<GetCategoryWithoutIncludeQuery>
{
    public GetCategoryWithoutIncludeQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CategoryErrors.CategoryWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}