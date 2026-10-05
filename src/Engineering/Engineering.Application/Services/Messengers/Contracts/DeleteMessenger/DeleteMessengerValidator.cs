namespace Engineering.Application.Services.Messengers.Contracts.DeleteMessenger;

public class DeleteMessengerValidator : AbstractValidator<DeleteMessengerRequest>
{
    public DeleteMessengerValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
    }
}