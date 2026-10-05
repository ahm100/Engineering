namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeStatus;

public class ChangeStatusTransportationRequestCommandValidator : AbstractValidator<ChangeStatusTransportationRequestCommand>
{
    public ChangeStatusTransportationRequestCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(TransportationRequestErrors.IdIsEmpty);
        RuleFor(oo => oo.Status).NotEmpty().WithError(TransportationRequestErrors.StatusIsEmpty);
        When(oo => oo.ManagerDescription != null, () =>
        {
            RuleFor(oo => oo.ManagerDescription).NotEmpty().WithError(TransportationRequestErrors.ManagerDescriptionIsEmpty);
        });
    }
}
