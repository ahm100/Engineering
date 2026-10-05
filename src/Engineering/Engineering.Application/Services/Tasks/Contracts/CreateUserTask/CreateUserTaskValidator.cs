namespace Engineering.Application.Services.Tasks.Contracts.CreateUserTask;

public class CreateUserTaskValidator
    : AbstractValidator<CreateUserTaskRequest>
{
    public CreateUserTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.TaskGroupId)
            .IsPositive(GlobalCmts.Id);

    }
}