namespace Engineering.Application.Services.Categories.Models.GetCategoryById;

public class GetCategoryByIdValidator : AbstractValidator<GetCategoryByIdRequest>
{
    public GetCategoryByIdValidator()
    {
        RuleFor(v => v.Id)
            .IsPositive(GlobalCmts.CategoryId);

    }
}