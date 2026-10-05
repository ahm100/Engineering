
namespace Engineering.Application.Services.Machineries.Models.CreateMachinery;

public class CreateMachineryValidator : AbstractValidator<CreateMachineryRequest>
{
    public CreateMachineryValidator()
    {
        RuleFor(oo => oo.MachineriesGroupId).NotNull().GreaterThanOrEqualTo(1).WithError(MachineryErrors.MachineriesGroupIsEmpty);
        RuleFor(oo => oo.MachineryName).NotEmpty().WithError(MachineryErrors.MachineryNameIsEmpty);
        RuleFor(oo => oo.MachineryCode).NotEmpty().WithError(MachineryErrors.MachineryCodeIsEmpty);
        RuleFor(oo => oo.MachineryName).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.MachineryCode).Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
        RuleFor(oo => oo.IsActive).NotNull().WithError(MachineryErrors.IsActiveIsEmpty);
    }
}
