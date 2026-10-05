namespace Engineering.Application.Services.Messengers.Contracts.GetMessengerById;

public class GetMessengerByIdValidator : AbstractValidator<GetMessengerByIdRequest>
{
    public GetMessengerByIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
    }
}
