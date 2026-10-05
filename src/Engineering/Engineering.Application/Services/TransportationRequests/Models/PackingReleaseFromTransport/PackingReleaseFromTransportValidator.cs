namespace Engineering.Application.Services.TransportationRequests.Models.PackingReleaseFromTransport;

public class PackingReleaseFromTransportValidator : AbstractValidator<PackingReleaseFromTransportRequest>
{
    public PackingReleaseFromTransportValidator()
    {
        RuleForEach(oo => oo.CargoIds)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .NotEmpty()
            .WithError(TransportationRequestErrors.CargoIsEmpty);

        RuleFor(oo => oo.Id)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .NotEmpty()
            .WithError(TransportationRequestErrors.IdIsEmpty);
    }
}