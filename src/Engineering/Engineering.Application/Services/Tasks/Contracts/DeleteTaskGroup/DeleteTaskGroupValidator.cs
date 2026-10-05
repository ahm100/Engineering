namespace Engineering.Application.Services.Tasks.Contracts.DeleteTaskGroup;

public class DeleteTaskGroupValidator
    : AbstractValidator<DeleteTaskGroupRequest>
{
    public DeleteTaskGroupValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}