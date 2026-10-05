namespace Engineering.Application.Services.Actions.Contracts.DeleteActions;

public class DeleteActionValidator : AbstractValidator<DeleteActionRequest>
{
    public DeleteActionValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
