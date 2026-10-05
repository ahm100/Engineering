
namespace Engineering.Application.Services.Transportations.Models.Update;

public class UpdateTransportationValidator : AbstractValidator<UpdateTransportationRequest>
{
    public UpdateTransportationValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationErrors.IdIsEmpty);
        RuleFor(oo => oo.TransportationName).NotEmpty().WithError(TransportationErrors.NameIsEmpty);
        RuleFor(oo => oo.TransportationCode).NotEmpty().WithError(TransportationErrors.CodeIsEmpty);
        RuleFor(oo => oo.IsPassenger).NotNull().WithError(TransportationErrors.IsPassengerIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(TransportationErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.TransportationType).IsInEnum().WithError(TransportationErrors.TransportationTypeIsEmpty);
        RuleFor(oo => oo.TransportationName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.TransportationCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}
