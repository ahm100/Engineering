namespace Engineering.Application.Services.Tasks.Contracts.DeleteUserTask;


public class DeleteUserTaskValidator
    : AbstractValidator<DeleteUserTaskRequest>
{
    public DeleteUserTaskValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
