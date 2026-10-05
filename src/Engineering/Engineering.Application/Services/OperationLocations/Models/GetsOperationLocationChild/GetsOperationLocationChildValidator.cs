namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationChild;

public class GetsOperationLocationChildValidator : AbstractValidator<GetsOperationLocationChildRequest>
{
    public GetsOperationLocationChildValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(OperationLocationErrors.IdIsEmpty);

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
