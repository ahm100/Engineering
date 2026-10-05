namespace Engineering.Application.Services.TransportationRequests.Commands.ChangeStatusToAccepted;

public class ChangeStatusToAcceptedTransportationRequestCommandValidator : AbstractValidator<ChangeStatusToAcceptedTransportationRequestCommand>
{
    public ChangeStatusToAcceptedTransportationRequestCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(TransportationRequestErrors.IdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}
