
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportLoadWeight;

public class UpdateTransportLoadWeightCommandValidator : AbstractValidator<UpdateTransportLoadWeightCommand>
{
    public UpdateTransportLoadWeightCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.RequestNotValid);
        RuleFor(oo => oo.LoadWeight).NotNull().GreaterThan(0).WithError(TransportationRequestErrors.LoadWeightIsEmpty);
    }
}
