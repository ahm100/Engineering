
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateAfterReforms;

public class UpdateAfterReformsCommandValidator : AbstractValidator<UpdateAfterReformsCommand>
{
    public UpdateAfterReformsCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.InValidTypeOfTransport);
    }
}
