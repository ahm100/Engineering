namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachinery;

public class UpdateRequestMachineryCommandValidator : AbstractValidator<UpdateRequestMachineryCommand>
{
    public UpdateRequestMachineryCommandValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().WithError(RequestMachineryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.TimeRequired).NotEmpty().WithError(RequestMachineryErrors.InValidTimeRequired);
        RuleFor(oo => oo.Unit).NotEmpty().WithError(RequestMachineryErrors.InValidUnit);
        RuleFor(oo => oo.Machinery).NotEmpty().WithError(RequestMachineryErrors.InValidMachinery);
        RuleFor(oo => oo.FromDate).NotEmpty().WithError(RequestMachineryErrors.InValidFromDate);
        RuleFor(oo => oo.ToDate).NotEmpty().WithError(RequestMachineryErrors.InValidToDate);
        RuleFor(oo => oo.RequestCount).NotEmpty().GreaterThan(0).WithError(RequestMachineryErrors.InValidRequestCount);
        RuleFor(oo => oo.MachineryIdentifier).NotEmpty().WithError(RequestMachineryErrors.InValidMachinery);
    }
}
