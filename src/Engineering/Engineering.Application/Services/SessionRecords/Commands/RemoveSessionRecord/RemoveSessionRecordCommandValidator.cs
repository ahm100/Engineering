namespace Engineering.Application.Services.SessionRecords.Commands.RemoveSessionRecord;

public class RemoveSessionRecordCommandValidator : AbstractValidator<RemoveSessionRecordCommand>
{
    public RemoveSessionRecordCommandValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
