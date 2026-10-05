namespace Engineering.Application.Services.SessionRecords.Commands.EditSessionRecord;

public class EditSessionRecordCommandValidator : AbstractValidator<EditSessionRecordCommand>
{
    public EditSessionRecordCommandValidator()
    {
        RuleFor(e => e.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
