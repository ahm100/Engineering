namespace Engineering.Application.Services.Messengers.Contracts.GetMessengerById;

public class GetMessengerChannelByIdValidator : AbstractValidator<GetMessengerChannelByIdRequest>
{
    public GetMessengerChannelByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
