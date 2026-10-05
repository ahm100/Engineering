namespace Engineering.Application.Services.ServiceInfos.Commands.InactiveService;

public class InactiveServiceInfoCommandValidator : AbstractValidator<InactiveServiceInfoCommand>
{
    public InactiveServiceInfoCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ServiceInfoErrors.IdIsEmpty);
    }
}