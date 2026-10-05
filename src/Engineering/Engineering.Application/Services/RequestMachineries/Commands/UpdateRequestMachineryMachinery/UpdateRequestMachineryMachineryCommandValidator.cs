namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryMachinery;

public class UpdateRequestMachineryMachineryCommandValidator : AbstractValidator<UpdateRequestMachineryMachineryCommand>
{
    public UpdateRequestMachineryMachineryCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.Machinery).NotEmpty().WithError(RequestMachineryErrors.InValidMachinery);
    }
}
