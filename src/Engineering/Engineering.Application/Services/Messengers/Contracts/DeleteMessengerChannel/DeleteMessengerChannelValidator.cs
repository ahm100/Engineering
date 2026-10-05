namespace Engineering.Application.Services.Messengers.Contracts.DeleteMessengerChannel;

public class DeleteMessengerChannelValidator : AbstractValidator<DeleteMessengerChannelRequest>
{
    public DeleteMessengerChannelValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}