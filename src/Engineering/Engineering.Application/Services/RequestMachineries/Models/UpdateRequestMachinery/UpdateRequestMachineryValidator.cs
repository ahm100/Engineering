namespace Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachinery;

public class UpdateRequestMachineryValidator : AbstractValidator<UpdateRequestMachineryRequest>
{
    public UpdateRequestMachineryValidator()
    {
        RuleFor(oo => oo.RequestMachineryId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestMachineryErrors.InValidRequestMachinery);
        RuleFor(oo => oo.TimeRequired).NotEmpty().WithError(RequestMachineryErrors.InValidTimeRequired);
        RuleFor(oo => oo.Unit).NotEmpty().WithError(RequestMachineryErrors.InValidUnit);
        RuleFor(oo => oo.MachineryId).NotNull().GreaterThanOrEqualTo(1).NotEmpty().WithError(RequestMachineryErrors.InValidMachinery);
        RuleFor(oo => oo.FromDate).NotEmpty().WithError(RequestMachineryErrors.InValidFromDate);
        RuleFor(oo => oo.ToDate).NotEmpty().WithError(RequestMachineryErrors.InValidToDate);
        RuleFor(oo => oo.RequestCount).NotEmpty().GreaterThan(0).WithError(RequestMachineryErrors.InValidRequestCount);
        RuleFor(oo => oo.MachineryIdentifier).NotEmpty().WithError(RequestMachineryErrors.InValidMachineryidentifier);
    }
}
