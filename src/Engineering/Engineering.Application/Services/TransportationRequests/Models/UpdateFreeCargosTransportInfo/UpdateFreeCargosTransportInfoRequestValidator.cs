namespace Engineering.Application.Services.TransportationRequests.Models.UpdateFreeCargosTransportInfo;

public class UpdateFreeCargosTransportInfoRequestValidator : AbstractValidator<UpdateFreeCargosTransportInfoRequest>
{
    public UpdateFreeCargosTransportInfoRequestValidator()
    {
        RuleFor(oo => oo.CargoId)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .NotEmpty()
            .WithError(TransportationRequestErrors.CargoIsEmpty);
    }
}