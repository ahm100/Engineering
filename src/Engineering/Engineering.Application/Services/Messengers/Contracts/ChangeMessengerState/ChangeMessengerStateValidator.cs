namespace Engineering.Application.Services.Messengers.Contracts.ChangeMessengerState
{
    public class ChangeMessengerStateValidator : AbstractValidator<ChangeMessengerStateRequest>
    {
        public ChangeMessengerStateValidator()
        {
            RuleForEach(c => c.Ids)
                .IsPositive(MessengerCmts.Messenger);
        }
    }
}
