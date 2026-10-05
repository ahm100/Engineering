using Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;

namespace Engineering.Application.Services.SessionRecords.Queries.GetSessionRecordDetail;

public class GetSessionRecordDetailValidator : AbstractValidator<GetSessionRecordDetailQuery>
{
    public GetSessionRecordDetailValidator()
    {
        RuleFor(e => e.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
