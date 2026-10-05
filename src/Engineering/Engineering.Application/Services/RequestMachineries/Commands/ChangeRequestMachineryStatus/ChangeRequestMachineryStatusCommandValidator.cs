namespace Engineering.Application.Services.RequestMachineries.Commands.ChangeRequestMachineryStatus;

public class ChangeRequestMachineryStatusCommandValidator : AbstractValidator<ChangeRequestMachineryStatusCommand>
{
    public ChangeRequestMachineryStatusCommandValidator()
    {
        RuleFor(oo => oo.Entity).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.Status).IsInEnum().WithError(RequestMachineryErrors.InValidStatus);
    }
}
