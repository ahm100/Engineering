namespace Engineering.Application.Services.Machineries.Models.UpdateMachinery;

public class UpdateMachineryValidator : AbstractValidator<UpdateMachineryRequest>
{
    public UpdateMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(MachineryErrors.IdIsEmpty);
        RuleFor(oo => oo.MachineriesGroupId).NotNull().GreaterThanOrEqualTo(1).WithError(MachineryErrors.MachineriesGroupIsEmpty);
        RuleFor(oo => oo.MachineryName).NotEmpty().WithError(MachineryErrors.MachineryNameIsEmpty);
        RuleFor(oo => oo.MachineryCode).NotEmpty().WithError(MachineryErrors.MachineryCodeIsEmpty);
        RuleFor(oo => oo.MachineryName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.MachineryCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.IsActive).NotNull().WithError(MachineryErrors.IsActiveIsEmpty);
    }
}
