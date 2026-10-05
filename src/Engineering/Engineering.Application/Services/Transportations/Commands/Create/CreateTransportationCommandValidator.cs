namespace Engineering.Application.Services.Transportations.Commands.Create;

public class CreateTransportationCommandValidator : AbstractValidator<CreateTransportationCommand>
{
    public CreateTransportationCommandValidator()
    {
        RuleFor(oo => oo.TransportationName).NotEmpty().WithError(TransportationErrors.NameIsEmpty);
        RuleFor(oo => oo.TransportationCode).NotEmpty().WithError(TransportationErrors.CodeIsEmpty);
        RuleFor(oo => oo.IsPassenger).NotNull().WithError(TransportationErrors.IsPassengerIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(TransportationErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.TransportationType).IsInEnum().WithError(TransportationErrors.TransportationTypeIsEmpty);
    }
}
