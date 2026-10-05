namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetsByOperationInfoId;

public class GetsByOperationInfoIdQueryValidator : AbstractValidator<GetsByOperationInfoIdQuery>
{
    public GetsByOperationInfoIdQueryValidator()
    {
        RuleFor(oo => oo.OprationInfoId)
            .IsPositive(GlobalCmts.Id);

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
