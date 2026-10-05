namespace Engineering.Application.Services.SessionRecords.Contracts.RemoveSessionRecord;

public class RemoveSessionRecordValidator : AbstractValidator<RemoveSessionRecordRequest>
{
    public RemoveSessionRecordValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
