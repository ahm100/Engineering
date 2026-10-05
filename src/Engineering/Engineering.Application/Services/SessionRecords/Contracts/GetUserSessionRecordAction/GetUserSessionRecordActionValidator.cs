namespace Engineering.Application.Services.SessionRecords.Contracts.GetUserSessionRecordAction;

public class GetUserSessionRecordActionValidator : AbstractValidator<GetUserSessionRecordActionRequest>
{
    public GetUserSessionRecordActionValidator()
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