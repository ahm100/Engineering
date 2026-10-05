namespace Engineering.Application.Services.Tasks.Contracts.UpdateUserTask;


public class UpdateUserTaskValidator
    : AbstractValidator<UpdateUserTaskRequest>
{
    public UpdateUserTaskValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.TaskGroupId)
            .IsPositive(GlobalCmts.Id);
    }
}