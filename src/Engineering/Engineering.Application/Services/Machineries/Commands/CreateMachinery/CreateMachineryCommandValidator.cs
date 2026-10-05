namespace Engineering.Application.Services.Machineries.Commands.CreateMachinery;

public class CreateMachineryCommandValidator : AbstractValidator<CreateMachineryCommand>
{
    public CreateMachineryCommandValidator()
    {
        RuleFor(oo => oo.MachineriesGroup).NotEmpty().WithError(MachineryErrors.MachineriesGroupIsEmpty);
        RuleFor(oo => oo.MachineryName).NotEmpty().WithError(MachineryErrors.MachineryNameIsEmpty);
        RuleFor(oo => oo.MachineryCode).NotEmpty().WithError(MachineryErrors.MachineryCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(MachineryErrors.IsActiveIsEmpty);
    }
}