namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocation;

public class GetsOperationLocationValidator : AbstractValidator<GetsOperationLocationRequest>
{
    public GetsOperationLocationValidator()
    {
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
