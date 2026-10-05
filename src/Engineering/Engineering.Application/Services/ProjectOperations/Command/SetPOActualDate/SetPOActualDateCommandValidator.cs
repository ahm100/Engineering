namespace Engineering.Application.Services.ProjectOperations.Command.SetPOActualDate;

public class SetPOActualDateCommandValidator : AbstractValidator<SetPOActualDateCommand>
{
    public SetPOActualDateCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
        RuleFor(oo => oo.DailyProjectOperationDate)
            .IsDate(DailyProjectOperationCmts.StartDate);
    }
}