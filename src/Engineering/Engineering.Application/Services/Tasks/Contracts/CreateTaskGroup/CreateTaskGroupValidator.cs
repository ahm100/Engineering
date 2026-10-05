namespace Engineering.Application.Services.Tasks.Contracts.CreateTaskGroup;


public class CreateTaskGroupValidator
    : AbstractValidator<CreateTaskGroupRequest>
{
    public CreateTaskGroupValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);
    }
}