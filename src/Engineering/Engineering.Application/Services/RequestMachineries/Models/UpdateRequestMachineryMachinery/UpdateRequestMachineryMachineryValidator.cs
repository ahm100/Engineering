namespace Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryMachinery;

public class UpdateRequestMachineryMachineryValidator : AbstractValidator<UpdateRequestMachineryMachineryRequest>
{
    public UpdateRequestMachineryMachineryValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.MachineryId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestMachineryErrors.InValidMachinery);
    }
}
