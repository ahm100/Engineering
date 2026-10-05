namespace Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.UpdateMachinery;

public class UpdateMachineryCommandValidator : AbstractValidator<UpdateMachineryCommand>
{
    public UpdateMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineryStandardErrors.MachineryUnitIdIsEmpty);
        RuleFor(oo => oo.Machinery).NotNull().WithError(MachineryStandardErrors.MachineryIdIsEmpty);
        RuleFor(oo => oo.MachineryNumber).GreaterThanOrEqualTo(0).WithError(MachineryStandardErrors.MachineryNumberIsEmpty);
        RuleFor(oo => oo.TimeSpant).NotNull().WithError(MachineryStandardErrors.TimeSpantIsEmpty);
    }
}