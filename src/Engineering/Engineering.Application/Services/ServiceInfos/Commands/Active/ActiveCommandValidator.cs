namespace Engineering.Application.Services.ServiceInfos.Commands.Active;

public class ActiveServiceInfoCommandValidator : AbstractValidator<ActiveServiceInfoCommand>
{
    public ActiveServiceInfoCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ServiceInfoErrors.IdIsEmpty);
    }
}