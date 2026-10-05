namespace Engineering.Application.Services.Categories.Models.StateChangerCategories;

public class StateChangerCategoriesValidator : AbstractValidator<StateChangerCategoriesRequest>
{
    public StateChangerCategoriesValidator()
    {
        RuleForEach(v => v.Ids)
            .IsPositive(GlobalCmts.CategoryId);
    }
}