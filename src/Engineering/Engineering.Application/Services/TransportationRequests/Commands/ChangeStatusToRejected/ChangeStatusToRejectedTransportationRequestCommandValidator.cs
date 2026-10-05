namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeStatusToRejected;

public class ChangeStatusToRejectedTransportationRequestCommandValidator : AbstractValidator<ChangeStatusToRejectedTransportationRequestCommand>
{
    public ChangeStatusToRejectedTransportationRequestCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
        When(oo => oo.ManagerDescription != null, () =>
        {
            RuleFor(oo => oo.ManagerDescription).NotEmpty().WithError(TransportationRequestErrors.ManagerDescriptionIsEmpty);
        });
    }
}
