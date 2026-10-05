namespace Engineering.Application.Services.Tasks.Contracts.GetTaskGroup;

public class GetTaskGroupValidator
    : AbstractValidator<GetTaskGroupRequest>
{
    public GetTaskGroupValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}