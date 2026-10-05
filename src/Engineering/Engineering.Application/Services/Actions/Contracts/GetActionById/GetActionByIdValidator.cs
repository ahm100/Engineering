namespace Engineering.Application.Services.Actions.Contracts.GetActionById;

public class GetActionByIdValidator : AbstractValidator<GetActionByIdRequest>
{
    public GetActionByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
