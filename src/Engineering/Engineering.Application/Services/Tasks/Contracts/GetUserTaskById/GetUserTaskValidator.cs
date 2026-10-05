namespace Engineering.Application.Services.Tasks.Contracts.GetUserTaskById;


public class GetUserTaskValidator
    : AbstractValidator<GetUserTaskRequest>
{
    public GetUserTaskValidator()
    {
        RuleFor(x => x.Id)
            .IsPositive(GlobalCmts.Id);
    }
}