namespace Engineering.Application.Services.SessionRecords.Contracts.EditSessionRecord;

public class EditSessionRecordValidator : AbstractValidator<EditSessionRecordRequest>
{
    public EditSessionRecordValidator()
    {
        RuleFor(e => e.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
