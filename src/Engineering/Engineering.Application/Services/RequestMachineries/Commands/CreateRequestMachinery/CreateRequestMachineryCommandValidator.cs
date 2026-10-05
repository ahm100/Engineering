namespace Engineering.Application.Services.RequestMachineries.Commands.CreateRequestMachinery;

public class CreateRequestMachineryCommandValidator : AbstractValidator<CreateRequestMachineryCommand>
{
    public CreateRequestMachineryCommandValidator()
    {
        RuleFor(oo => oo.TimeRequired).NotEmpty().WithError(RequestMachineryErrors.InValidTimeRequired);
        RuleFor(oo => oo.Unit).IsInEnum().WithError(RequestMachineryErrors.InValidUnit);
        RuleFor(oo => oo.Project).NotEmpty().WithError(RequestMachineryErrors.InValidProject);
        RuleFor(oo => oo.RequestCount).NotEmpty().GreaterThan(0).WithError(RequestMachineryErrors.InValidRequestCount);
        RuleFor(oo => oo.FromDate).NotNull().WithError(RequestMachineryErrors.InValidFromDate);
        RuleFor(oo => oo.ToDate).NotNull().WithError(RequestMachineryErrors.InValidToDate);
    }
}
