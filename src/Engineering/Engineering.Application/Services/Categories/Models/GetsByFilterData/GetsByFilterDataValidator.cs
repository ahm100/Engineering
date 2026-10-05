namespace Engineering.Application.Services.Categories.Models.GetsByFilterData;

public class GetsByFilterDataValidator : AbstractValidator<GetsByFilterDataRequest>
{
    public GetsByFilterDataValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}