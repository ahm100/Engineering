namespace Engineering.Application.Services.SessionRecords.Commands.CreateSessionRecord;

public class CreateSessionRecordCommandValidator : AbstractValidator<CreateSessionRecordCommand>
{
    public CreateSessionRecordCommandValidator()
    {
        RuleFor(e => e.TitleFa)
            .IsFullString(GlobalCmts.TitleFa, 250, RegexPatterns.SafeText, RegexPatterns.SafeTextTitle);

        RuleFor(e => e.Category)
            .IsEnum(GlobalCmts.Category);

        RuleFor(e => e.Type)
            .IsEnum(SessionRecordCmts.SessionType);

        RuleFor(e => e.SessionDate)
            .NotNull().WithError(GlobalErrors.RequiredEmpty(SessionRecordCmts.SessionDate));

        RuleFor(e => e.StartTime)
            .NotNull().WithError(GlobalErrors.RequiredEmpty(SessionRecordCmts.StartTime));

        RuleFor(e => e.EndTime)
            .NotNull().WithError(GlobalErrors.RequiredEmpty(SessionRecordCmts.EndTime));

    }
}
