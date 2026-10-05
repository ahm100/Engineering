namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeCargoToSecurityConfirm;

public class ChangeCargoToSecurityConfirmCommandValidator : AbstractValidator<ChangeCargoToSecurityConfirmCommand>
{
    public ChangeCargoToSecurityConfirmCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}
