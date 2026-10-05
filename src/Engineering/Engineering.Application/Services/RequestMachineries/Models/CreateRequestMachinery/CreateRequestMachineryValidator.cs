namespace Engineering.Application.Services.RequestMachineries.Models.CreateRequestMachinery;

public class CreateRequestMachineryValidator : AbstractValidator<CreateRequestMachineryRequest>
{
    public CreateRequestMachineryValidator()
    {
        RuleFor(oo => oo.TimeRequired).NotEmpty().WithError(RequestMachineryErrors.InValidTimeRequired);
        RuleFor(oo => oo.Unit).IsInEnum().WithError(RequestMachineryErrors.InValidUnit);
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1)
            .WithError(RequestMachineryErrors.InValidProject);
        RuleFor(oo => oo.RequestCount).NotEmpty().GreaterThan(0).WithError(RequestMachineryErrors.InValidRequestCount);
        RuleFor(oo => oo.FromDate).NotNull().WithError(RequestMachineryErrors.InValidFromDate);
        RuleFor(oo => oo.ToDate).NotNull().WithError(RequestMachineryErrors.InValidToDate);
    }
}
