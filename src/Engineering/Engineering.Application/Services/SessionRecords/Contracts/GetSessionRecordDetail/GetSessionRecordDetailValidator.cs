namespace Engineering.Application.Services.SessionRecords.Contracts.GetSessionRecordDetail;

public class GetSessionRecordDetailValidator : AbstractValidator<GetSessionRecordDetailRequest>
{
    public GetSessionRecordDetailValidator()
    {
        RuleFor(e => e.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
