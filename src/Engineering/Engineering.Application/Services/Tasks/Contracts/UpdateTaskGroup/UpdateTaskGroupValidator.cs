namespace Engineering.Application.Services.Tasks.Contracts.UpdateTaskGroup;


public class UpdateTaskGroupValidator
    : AbstractValidator<UpdateTaskGroupRequest>
{
    public UpdateTaskGroupValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);
    }
}