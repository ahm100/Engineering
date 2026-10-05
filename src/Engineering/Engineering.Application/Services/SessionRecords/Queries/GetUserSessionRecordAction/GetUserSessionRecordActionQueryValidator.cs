using System.Data;

namespace Engineering.Application.Services.SessionRecords.Queries.GetUserSessionRecordAction;

public class GetUserSessionRecordActionQueryValidator : AbstractValidator<GetUserSessionRecordActionQuery>
{
    public GetUserSessionRecordActionQueryValidator()
    {
        RuleFor(e => e.UserId)
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
