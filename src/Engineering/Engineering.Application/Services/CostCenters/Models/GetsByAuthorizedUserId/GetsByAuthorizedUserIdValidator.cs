namespace Engineering.Application.Services.CostCenters.Models.GetsByAuthorizedUserId;

public class GetsByAuthorizedUserIdValidator : AbstractValidator<GetsByAuthorizedUserIdRequest>
{
    public GetsByAuthorizedUserIdValidator()
    {
        RuleFor(oo => oo.UserId)
            .IsPositive(CCenterCmts.AuthorizedUserId);
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
