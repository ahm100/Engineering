namespace Engineering.Application.Services.Messengers.Contracts.CreateMessenger;

public class CreateMessengerValidator : AbstractValidator<CreateMessengerRequest>
{
    public CreateMessengerValidator()
    {
        RuleFor(oo => oo.Type).IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.TargetType).IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.TargetId).IsPositive(GlobalCmts.Id);
    }
}