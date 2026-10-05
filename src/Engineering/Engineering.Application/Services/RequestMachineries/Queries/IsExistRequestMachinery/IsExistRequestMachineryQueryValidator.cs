namespace Engineering.Application.Services.RequestMachineries.Queries.IsExistRequestMachinery;

public class IsExistRequestMachineryQueryValidator : AbstractValidator<IsExistRequestMachineryQuery>
{
    public IsExistRequestMachineryQueryValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().WithError(RequestMachineryErrors.InValidProject);
        RuleFor(oo => oo.MachineryId).NotNull().WithError(RequestMachineryErrors.InValidMachinery);
        RuleFor(oo => oo.TimeRequired).NotNull().WithError(RequestMachineryErrors.InValidTimeRequired);
        RuleFor(oo => oo.RequestCount).NotNull().WithError(RequestMachineryErrors.InValidRequestCount);
    }
}
