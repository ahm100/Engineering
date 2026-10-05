
namespace Engineering.Application.Services.Transportations.Models.Create;

public class CreateTransportationValidator : AbstractValidator<CreateTransportationRequest>
{
    public CreateTransportationValidator()
    {
        RuleFor(oo => oo.TransportationName).NotEmpty().WithError(TransportationErrors.NameIsEmpty);
        RuleFor(oo => oo.TransportationCode).NotEmpty().WithError(TransportationErrors.CodeIsEmpty);
        RuleFor(oo => oo.IsPassenger).NotNull().WithError(TransportationErrors.IsPassengerIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(TransportationErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.TransportationType).IsInEnum().WithError(TransportationErrors.TransportationTypeIsEmpty);
        RuleFor(oo => oo.TransportationName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.TransportationCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}
