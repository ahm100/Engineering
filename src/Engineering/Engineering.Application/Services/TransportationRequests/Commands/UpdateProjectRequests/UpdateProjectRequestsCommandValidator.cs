
namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateProjectRequests;

public class UpdateProjectRequestsCommandValidator : AbstractValidator<UpdateProjectRequestsCommand>
{
    public UpdateProjectRequestsCommandValidator()
    {
        RuleFor(oo => oo.TransportationRequest).NotNull().WithError(TransportationRequestErrors.InValidTypeOfTransport);
        RuleForEach(oo => oo.Projects).NotNull().WithError(TransportationRequestErrors.UnValidProjects);
    }
}
