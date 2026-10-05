namespace Engineering.Application.Services.Machineries.Commands.UpdateMachinery;

public class UpdateMachineryCommandValidator : AbstractValidator<UpdateMachineryCommand>
{
    public UpdateMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineryErrors.IdIsEmpty);
        RuleFor(oo => oo.MachineriesGroup).NotEmpty().WithError(MachineryErrors.MachineriesGroupIsEmpty);
        RuleFor(oo => oo.MachineryName).NotEmpty().WithError(MachineryErrors.MachineryNameIsEmpty);
        RuleFor(oo => oo.MachineryCode).NotEmpty().WithError(MachineryErrors.MachineryCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(MachineryErrors.IsActiveIsEmpty);
    }
}