namespace Engineering.Application.Services.Messengers.Contracts.CreateMessengerChannel;

public class CreateMessengerChannelValidator : AbstractValidator<CreateMessengerChannelRequest>
{
    public CreateMessengerChannelValidator()
    {
        RuleFor(oo => oo.Type).IsEnum(GlobalCmts.Type);
        RuleFor(oo => oo.ChatId).IsRequiredString(GlobalCmts.Id);
        RuleFor(oo => oo.MessengerId).IsPositive(GlobalCmts.Id);
    }
}